using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace NLog.ServiceBus
{
    internal class QueueClient : IMessageSender
    {
        private readonly Microsoft.ServiceBus.Messaging.QueueClient _baseClient;

        private QueueClient(Microsoft.ServiceBus.Messaging.QueueClient baseClient)
        {
            _baseClient = baseClient;
        }

        public RetryPolicy RetryPolicy
        {
            get { return _baseClient.RetryPolicy; }
            set { _baseClient.RetryPolicy = value; }
        }

        public ReceiveMode Mode
        {
            get { return _baseClient.Mode; }
        }

        public int PrefetchCount
        {
            get { return _baseClient.PrefetchCount; }
            set { _baseClient.PrefetchCount = value; }
        }

        public string Path
        {
            get { return _baseClient.Path; }
        }

        public MessagingFactory MessagingFactory
        {
            get { return _baseClient.MessagingFactory; }
        }

        public Task CloseAsync()
        {
            return _baseClient.CloseAsync();
        }

        public bool IsClosed
        {
            get { return _baseClient.IsClosed; }
        }

        public void Send(BrokeredMessage message)
        {
            _baseClient.Send(message);
        }

        public void SendBatch(IEnumerable<BrokeredMessage> messages)
        {
            _baseClient.SendBatch(messages);
        }

        public IAsyncResult BeginSend(BrokeredMessage message, AsyncCallback callback, object state)
        {
            return _baseClient.BeginSend(message, callback, state);
        }

        public IAsyncResult BeginSendBatch(IEnumerable<BrokeredMessage> messages, AsyncCallback callback, object state)
        {
            return _baseClient.BeginSendBatch(messages, callback, state);
        }

        public void EndSend(IAsyncResult result)
        {
            _baseClient.EndSend(result);
        }

        public void EndSendBatch(IAsyncResult result)
        {
            _baseClient.EndSendBatch(result);
        }

        public void Abort()
        {
            _baseClient.Abort();
        }

        public void Close()
        {
            _baseClient.Close();
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            return _baseClient.BeginClose(callback, state);
        }

        public void EndClose(IAsyncResult result)
        {
            _baseClient.EndClose(result);
        }

        public void OnMessage(Action<BrokeredMessage> callback)
        {
            _baseClient.OnMessage(callback);
        }

        public void OnMessage(Action<BrokeredMessage> callback, OnMessageOptions onMessageOptions)
        {
            _baseClient.OnMessage(callback, onMessageOptions);
        }

        public void OnMessageAsync(Func<BrokeredMessage, Task> callback)
        {
            _baseClient.OnMessageAsync(callback);
        }

        public void OnMessageAsync(Func<BrokeredMessage, Task> callback, OnMessageOptions onMessageOptions)
        {
            _baseClient.OnMessageAsync(callback, onMessageOptions);
        }

        public MessageSession AcceptMessageSession()
        {
            return _baseClient.AcceptMessageSession();
        }

        public MessageSession AcceptMessageSession(string sessionId)
        {
            return _baseClient.AcceptMessageSession(sessionId);
        }

        public MessageSession AcceptMessageSession(TimeSpan serverWaitTime)
        {
            return _baseClient.AcceptMessageSession(serverWaitTime);
        }

        public MessageSession AcceptMessageSession(string sessionId, TimeSpan serverWaitTime)
        {
            return _baseClient.AcceptMessageSession(sessionId, serverWaitTime);
        }

        public Task<MessageSession> AcceptMessageSessionAsync()
        {
            return _baseClient.AcceptMessageSessionAsync();
        }

        public Task<MessageSession> AcceptMessageSessionAsync(string sessionId)
        {
            return _baseClient.AcceptMessageSessionAsync(sessionId);
        }

        public Task<MessageSession> AcceptMessageSessionAsync(TimeSpan serverWaitTime)
        {
            return _baseClient.AcceptMessageSessionAsync(serverWaitTime);
        }

        public Task<MessageSession> AcceptMessageSessionAsync(string sessionId, TimeSpan serverWaitTime)
        {
            return _baseClient.AcceptMessageSessionAsync(sessionId, serverWaitTime);
        }

        public IAsyncResult BeginAcceptMessageSession(AsyncCallback callback, object state)
        {
            return _baseClient.BeginAcceptMessageSession(callback, state);
        }

        public IAsyncResult BeginAcceptMessageSession(string sessionId, AsyncCallback callback, object state)
        {
            return _baseClient.BeginAcceptMessageSession(sessionId, callback, state);
        }

        public IAsyncResult BeginAcceptMessageSession(TimeSpan serverWaitTime, AsyncCallback callback, object state)
        {
            return _baseClient.BeginAcceptMessageSession(serverWaitTime, callback, state);
        }

        public IAsyncResult BeginAcceptMessageSession(string sessionId, TimeSpan timeout, AsyncCallback callback,
            object state)
        {
            return _baseClient.BeginAcceptMessageSession(sessionId, timeout, callback, state);
        }

        public MessageSession EndAcceptMessageSession(IAsyncResult result)
        {
            return _baseClient.EndAcceptMessageSession(result);
        }

        public IEnumerable<MessageSession> GetMessageSessions()
        {
            return _baseClient.GetMessageSessions();
        }

        public IEnumerable<MessageSession> GetMessageSessions(DateTime lastUpdatedTime)
        {
            return _baseClient.GetMessageSessions(lastUpdatedTime);
        }

        public Task<IEnumerable<MessageSession>> GetMessageSessionsAsync()
        {
            return _baseClient.GetMessageSessionsAsync();
        }

        public Task<IEnumerable<MessageSession>> GetMessageSessionsAsync(DateTime lastUpdatedTime)
        {
            return _baseClient.GetMessageSessionsAsync(lastUpdatedTime);
        }

        public IAsyncResult BeginGetMessageSessions(AsyncCallback callback, object state)
        {
            return _baseClient.BeginGetMessageSessions(callback, state);
        }

        public IAsyncResult BeginGetMessageSessions(DateTime lastUpdatedTime, AsyncCallback callback, object state)
        {
            return _baseClient.BeginGetMessageSessions(lastUpdatedTime, callback, state);
        }

        public IEnumerable<MessageSession> EndGetMessageSessions(IAsyncResult result)
        {
            return _baseClient.EndGetMessageSessions(result);
        }

        public void Abandon(Guid lockToken)
        {
            _baseClient.Abandon(lockToken);
        }

        public void Abandon(Guid lockToken, IDictionary<string, object> propertiesToModify)
        {
            _baseClient.Abandon(lockToken, propertiesToModify);
        }

        public Task AbandonAsync(Guid lockToken)
        {
            return _baseClient.AbandonAsync(lockToken);
        }

        public Task AbandonAsync(Guid lockToken, IDictionary<string, object> propertiesToModify)
        {
            return _baseClient.AbandonAsync(lockToken, propertiesToModify);
        }

        public IAsyncResult BeginAbandon(Guid lockToken, AsyncCallback callback, object state)
        {
            return _baseClient.BeginAbandon(lockToken, callback, state);
        }

        public IAsyncResult BeginAbandon(Guid lockToken, IDictionary<string, object> propertiesToModify,
            AsyncCallback callback, object state)
        {
            return _baseClient.BeginAbandon(lockToken, propertiesToModify, callback, state);
        }

        public void EndAbandon(IAsyncResult result)
        {
            _baseClient.EndAbandon(result);
        }

        public void Complete(Guid lockToken)
        {
            _baseClient.Complete(lockToken);
        }

        public Task CompleteAsync(Guid lockToken)
        {
            return _baseClient.CompleteAsync(lockToken);
        }

        public void CompleteBatch(IEnumerable<Guid> lockTokens)
        {
            _baseClient.CompleteBatch(lockTokens);
        }

        public Task CompleteBatchAsync(IEnumerable<Guid> lockTokens)
        {
            return _baseClient.CompleteBatchAsync(lockTokens);
        }

        public IAsyncResult BeginComplete(Guid lockToken, AsyncCallback callback, object state)
        {
            return _baseClient.BeginComplete(lockToken, callback, state);
        }

        public IAsyncResult BeginCompleteBatch(IEnumerable<Guid> lockTokens, AsyncCallback callback, object state)
        {
            return _baseClient.BeginCompleteBatch(lockTokens, callback, state);
        }

        public void EndComplete(IAsyncResult result)
        {
            _baseClient.EndComplete(result);
        }

        public void EndCompleteBatch(IAsyncResult result)
        {
            _baseClient.EndCompleteBatch(result);
        }

        public void Defer(Guid lockToken)
        {
            _baseClient.Defer(lockToken);
        }

        public void Defer(Guid lockToken, IDictionary<string, object> propertiesToModify)
        {
            _baseClient.Defer(lockToken, propertiesToModify);
        }

        public Task DeferAsync(Guid lockToken)
        {
            return _baseClient.DeferAsync(lockToken);
        }

        public Task DeferAsync(Guid lockToken, IDictionary<string, object> propertiesToModify)
        {
            return _baseClient.DeferAsync(lockToken, propertiesToModify);
        }

        public IAsyncResult BeginDefer(Guid lockToken, AsyncCallback callback, object state)
        {
            return _baseClient.BeginDefer(lockToken, callback, state);
        }

        public IAsyncResult BeginDefer(Guid lockToken, IDictionary<string, object> propertiesToModify,
            AsyncCallback callback, object state)
        {
            return _baseClient.BeginDefer(lockToken, propertiesToModify, callback, state);
        }

        public void EndDefer(IAsyncResult result)
        {
            _baseClient.EndDefer(result);
        }

        public void DeadLetter(Guid lockToken)
        {
            _baseClient.DeadLetter(lockToken);
        }

        public void DeadLetter(Guid lockToken, IDictionary<string, object> propertiesToModify)
        {
            _baseClient.DeadLetter(lockToken, propertiesToModify);
        }

        public void DeadLetter(Guid lockToken, string deadLetterReason, string deadLetterErrorDescription)
        {
            _baseClient.DeadLetter(lockToken, deadLetterReason, deadLetterErrorDescription);
        }

        public Task DeadLetterAsync(Guid lockToken)
        {
            return _baseClient.DeadLetterAsync(lockToken);
        }

        public Task DeadLetterAsync(Guid lockToken, IDictionary<string, object> propertiesToModify)
        {
            return _baseClient.DeadLetterAsync(lockToken, propertiesToModify);
        }

        public Task DeadLetterAsync(Guid lockToken, string deadLetterReason, string deadLetterErrorDescription)
        {
            return _baseClient.DeadLetterAsync(lockToken, deadLetterReason, deadLetterErrorDescription);
        }

        public IAsyncResult BeginDeadLetter(Guid lockToken, AsyncCallback callback, object state)
        {
            return _baseClient.BeginDeadLetter(lockToken, callback, state);
        }

        public IAsyncResult BeginDeadLetter(Guid lockToken, IDictionary<string, object> propertiesToModify,
            AsyncCallback callback, object state)
        {
            return _baseClient.BeginDeadLetter(lockToken, propertiesToModify, callback, state);
        }

        public IAsyncResult BeginDeadLetter(Guid lockToken, string deadLetterReason, string deadLetterErrorDescription,
            AsyncCallback callback, object state)
        {
            return _baseClient.BeginDeadLetter(lockToken, deadLetterReason, deadLetterErrorDescription, callback, state);
        }

        public void EndDeadLetter(IAsyncResult result)
        {
            _baseClient.EndDeadLetter(result);
        }

        public BrokeredMessage Receive()
        {
            return _baseClient.Receive();
        }

        public IEnumerable<BrokeredMessage> ReceiveBatch(int messageCount)
        {
            return _baseClient.ReceiveBatch(messageCount);
        }

        public BrokeredMessage Receive(TimeSpan serverWaitTime)
        {
            return _baseClient.Receive(serverWaitTime);
        }

        public Task<BrokeredMessage> ReceiveAsync()
        {
            return _baseClient.ReceiveAsync();
        }

        public Task<BrokeredMessage> ReceiveAsync(TimeSpan serverWaitTime)
        {
            return _baseClient.ReceiveAsync(serverWaitTime);
        }

        public Task<BrokeredMessage> ReceiveAsync(long sequenceNumber)
        {
            return _baseClient.ReceiveAsync(sequenceNumber);
        }

        public IEnumerable<BrokeredMessage> ReceiveBatch(int messageCount, TimeSpan serverWaitTime)
        {
            return _baseClient.ReceiveBatch(messageCount, serverWaitTime);
        }

        public BrokeredMessage Receive(long sequenceNumber)
        {
            return _baseClient.Receive(sequenceNumber);
        }

        public IEnumerable<BrokeredMessage> ReceiveBatch(IEnumerable<long> sequenceNumbers)
        {
            return _baseClient.ReceiveBatch(sequenceNumbers);
        }

        public Task<IEnumerable<BrokeredMessage>> ReceiveBatchAsync(int messageCount)
        {
            return _baseClient.ReceiveBatchAsync(messageCount);
        }

        public Task<IEnumerable<BrokeredMessage>> ReceiveBatchAsync(int messageCount, TimeSpan serverWaitTime)
        {
            return _baseClient.ReceiveBatchAsync(messageCount, serverWaitTime);
        }

        public Task<IEnumerable<BrokeredMessage>> ReceiveBatchAsync(IEnumerable<long> sequenceNumbers)
        {
            return _baseClient.ReceiveBatchAsync(sequenceNumbers);
        }

        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {
            return _baseClient.BeginReceive(callback, state);
        }

        public IAsyncResult BeginReceiveBatch(int messageCount, AsyncCallback callback, object state)
        {
            return _baseClient.BeginReceiveBatch(messageCount, callback, state);
        }

        public IAsyncResult BeginReceive(TimeSpan serverWaitTime, AsyncCallback callback, object state)
        {
            return _baseClient.BeginReceive(serverWaitTime, callback, state);
        }

        public IAsyncResult BeginReceiveBatch(int messageCount, TimeSpan serverWaitTime, AsyncCallback callback,
            object state)
        {
            return _baseClient.BeginReceiveBatch(messageCount, serverWaitTime, callback, state);
        }

        public IAsyncResult BeginReceive(long sequenceNumber, AsyncCallback callback, object state)
        {
            return _baseClient.BeginReceive(sequenceNumber, callback, state);
        }

        public IAsyncResult BeginReceiveBatch(IEnumerable<long> sequenceNumbers, AsyncCallback callback, object state)
        {
            return _baseClient.BeginReceiveBatch(sequenceNumbers, callback, state);
        }

        public BrokeredMessage EndReceive(IAsyncResult result)
        {
            return _baseClient.EndReceive(result);
        }

        public IEnumerable<BrokeredMessage> EndReceiveBatch(IAsyncResult result)
        {
            return _baseClient.EndReceiveBatch(result);
        }

        public Task SendAsync(BrokeredMessage message)
        {
            return _baseClient.SendAsync(message);
        }

        public Task SendBatchAsync(IEnumerable<BrokeredMessage> messages)
        {
            return _baseClient.SendBatchAsync(messages);
        }

        public BrokeredMessage Peek()
        {
            return _baseClient.Peek();
        }

        public BrokeredMessage Peek(long fromSequenceNumber)
        {
            return _baseClient.Peek(fromSequenceNumber);
        }

        public Task<BrokeredMessage> PeekAsync()
        {
            return _baseClient.PeekAsync();
        }

        public Task<BrokeredMessage> PeekAsync(long fromSequenceNumber)
        {
            return _baseClient.PeekAsync(fromSequenceNumber);
        }

        public IEnumerable<BrokeredMessage> PeekBatch(int messageCount)
        {
            return _baseClient.PeekBatch(messageCount);
        }

        public IEnumerable<BrokeredMessage> PeekBatch(long fromSequenceNumber, int messageCount)
        {
            return _baseClient.PeekBatch(fromSequenceNumber, messageCount);
        }

        public Task<IEnumerable<BrokeredMessage>> PeekBatchAsync(int messageCount)
        {
            return _baseClient.PeekBatchAsync(messageCount);
        }

        public Task<IEnumerable<BrokeredMessage>> PeekBatchAsync(long fromSequenceNumber, int messageCount)
        {
            return _baseClient.PeekBatchAsync(fromSequenceNumber, messageCount);
        }

        public IAsyncResult BeginPeek(AsyncCallback callback, object state)
        {
            return _baseClient.BeginPeek(callback, state);
        }

        public IAsyncResult BeginPeek(long fromSequenceNumber, AsyncCallback callback, object state)
        {
            return _baseClient.BeginPeek(fromSequenceNumber, callback, state);
        }

        public IAsyncResult BeginPeekBatch(int messageCount, AsyncCallback callback, object state)
        {
            return _baseClient.BeginPeekBatch(messageCount, callback, state);
        }

        public IAsyncResult BeginPeekBatch(long fromSequenceNumber, int messageCount, AsyncCallback callback,
            object state)
        {
            return _baseClient.BeginPeekBatch(fromSequenceNumber, messageCount, callback, state);
        }

        public IEnumerable<BrokeredMessage> EndPeekBatch(IAsyncResult result)
        {
            return _baseClient.EndPeekBatch(result);
        }

        public BrokeredMessage EndPeek(IAsyncResult result)
        {
            return _baseClient.EndPeek(result);
        }

        public static IMessageSender Create(string path)
        {
            return new QueueClient(Microsoft.ServiceBus.Messaging.QueueClient.Create(path));
        }

        public static IMessageSender CreateFromConnectionString(string connectionString, string path)
        {
            return
                new QueueClient(Microsoft.ServiceBus.Messaging.QueueClient.CreateFromConnectionString(connectionString,
                    path));
        }
    }
}