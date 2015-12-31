using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSSB = Microsoft.ServiceBus.Messaging;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;
using NLog.Targets;

namespace NLog.ServiceBus
{
    [Target("ServiceBus")]
    public sealed class ServiceBusTarget : TargetWithLayout
    {
        public string ConnectionStringAppKeyName { get; set; }

        public string ConnectionString { get; set; }

        [RequiredParameter]
        public string TopicOrQueueName { get; set; }

        [ArrayParameter(typeof(KeyLayoutAttribute), "properties")]
        public IList<KeyLayoutAttribute> Properties { get; private set; }

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _configured;
        private MSSB.MessagingFactory _messagingFactory;
        private MSSB.MessageSender _messageSender;
        private readonly Timer _timer;

        /// <summary>
        /// track submitted tasks
        /// </summary>
        private readonly ISet<Task> _outstandingTasks = new HashSet<Task>();

        /// <summary>
        /// flag to indicate the target is closing ... don't create any SB clients or other shenanigans
        /// </summary>
        private bool _isClosing;

        public ServiceBusTarget()
        {
            Layout = "${message}";
            Properties = new List<KeyLayoutAttribute>();
            _timer = new Timer(OnTimer, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void OnTimer(object state)
        {
            // remove completed tasks from tracking HashSet
            var tasks = _outstandingTasks.Where(t => t.IsCanceled || t.IsCompleted || t.IsFaulted).ToArray();
            foreach (var task in tasks)
            {
                try
                {
                    _outstandingTasks.Remove(task);
                }
                catch
                {
                    // intentionally empty
                }
            }

            if (_outstandingTasks.Count < 1) StopTimer();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Dispose();
                _cancellationTokenSource.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void CloseTarget()
        {
            _isClosing = true;
            _cancellationTokenSource.Cancel();
            CloseSender();
            CloseMessagingFactory();
            base.CloseTarget();
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            if (!ConfigureTarget()) return;
            var brokeredMessage = CreateBrokeredMessage(logEvent.LogEvent);
            var continuation = logEvent.Continuation;

            // configure publish action
            Action<MSSB.BrokeredMessage, AsyncContinuation> publishAction =
                (bm, ac) =>
                {
                    if (_isClosing || _messageSender == null || _messageSender.IsClosed) return;

                    _outstandingTasks.Add(_messageSender.SendAsync(bm).ContinueWith(task =>
                    {
                        var ex = task.Exception;
                        if (ex != null)
                        {
                            InternalLogger.Error("Could not send message [{0}]", ex);
                            EnqueueFailedMessage(bm);
                            ac(ex);
                        }
                    },
                    _cancellationTokenSource.Token,
                    TaskContinuationOptions.AttachedToParent,
                    TaskScheduler.Current)); 
                };


            if (_messageSender == null || _messageSender.IsClosed)
            {
                _outstandingTasks.Add(CreateClient().ContinueWith(task =>
                {

                    var ex = task.Exception;
                    if (ex != null)
                    {
                        InternalLogger.Error("Could not Create a MessageSender [{0}]", ex);
                        continuation(ex);
                        return;
                    }

                    if (_messageSender == null || _messageSender.IsClosed) return;

                    publishAction(brokeredMessage, continuation);
                }, _cancellationTokenSource.Token));
            }
            else
            {
                publishAction(brokeredMessage, continuation);
            }
            
            StartTimer();
        }

        private void StartTimer()
        {
            _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        private void StopTimer()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        protected override void FlushAsync(AsyncContinuation asyncContinuation)
        {
            Task.WaitAll(_outstandingTasks.ToArray(), Convert.ToInt32( TimeSpan.FromSeconds(5).TotalMilliseconds), _cancellationTokenSource.Token);
            asyncContinuation(null);
        }

        private bool ConfigureTarget()
        {
            if (_isClosing) return false;
            if (_configured) return _configured;

            if (string.IsNullOrEmpty(ConnectionString))
            {
                if (!string.IsNullOrEmpty(ConnectionStringAppKeyName))
                {
                    ConnectionString = System.Configuration.ConfigurationManager.AppSettings[ConnectionStringAppKeyName];
                }
            }

            return _configured = true;
        }

        private void EnqueueFailedMessage(MSSB.BrokeredMessage brokeredMessage)
        {
            if (_isClosing) return;

            // TODO
        }

        private MSSB.BrokeredMessage CreateBrokeredMessage(LogEventInfo log)
        {
            var message = Layout.Render(log);
            var props = Properties.Render(log);

            var bm = new MSSB.BrokeredMessage(message);

            foreach (var kv in props)
            {
                bm.Properties.Add(kv.Key, kv.Value);
            }

            return bm;
        }

        private async Task CreateClient()
        {
            if (_isClosing) return;

            if (_messagingFactory == null || _messagingFactory.IsClosed)
            {
                _messagingFactory = await Task<MSSB.MessagingFactory>.Factory
                    .StartNew(() => _isClosing ? null : string.IsNullOrEmpty(ConnectionString)
                        ? MSSB.MessagingFactory.Create()
                        : MSSB.MessagingFactory.CreateFromConnectionString(ConnectionString));
            }

            if (_messagingFactory != null)
                if (_messageSender == null || _messageSender.IsClosed)
                {
                    _messageSender = await _messagingFactory.CreateMessageSenderAsync(TopicOrQueueName);
                }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void CloseSender()
        {
            if (_messageSender == null) return;
            if (_messageSender.IsClosed)
            {
                _messageSender = null;
                return;
            }

            try
            {
                _messageSender
                    .CloseAsync()
                    .ContinueWith(task =>
                    {
                        var ex = task.Exception;
                        if (ex != null)
                        {
                            InternalLogger.Error("Exception closing MessageSender [{0}]", ex);
                        }

                        _messageSender = null;
                    });
            }
            catch (Exception ex)
            {
                InternalLogger.Error("Couldn't close MessageSender [{0}]", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void CloseMessagingFactory()
        {
            if (_messagingFactory == null) return;
            if (_messagingFactory.IsClosed)
            {
                _messagingFactory = null;
                return;
            }

            try
            {
                _messagingFactory
                    .CloseAsync()
                    .ContinueWith(task =>
                    {
                        var ex = task.Exception;
                        if (ex != null)
                        {
                            InternalLogger.Error("Exception closing MessagingFactory [{0}]", ex);
                        }

                        _messagingFactory = null;
                    });
            }
            catch (Exception ex)
            {
                InternalLogger.Error("Couldn't close MessagingFactory [{0}]", ex);
            }
        }
    }


}
