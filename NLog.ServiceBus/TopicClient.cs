using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace NLog.ServiceBus
{
    internal class TopicClient : IMessageSender
    {
        public void Abort()
        {
            _baseClient.Abort();
        }

        public void Close()
        {
            _baseClient.Close();
        }

        public Task CloseAsync()
        {
            return _baseClient.CloseAsync();
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            return _baseClient.BeginClose(callback, state);
        }

        public void EndClose(IAsyncResult result)
        {
            _baseClient.EndClose(result);
        }

        public RetryPolicy RetryPolicy
        {
            get { return _baseClient.RetryPolicy; }
            set { _baseClient.RetryPolicy = value; }
        }

        public bool IsClosed
        {
            get { return _baseClient.IsClosed; }
        }

        public void Send(Microsoft.ServiceBus.Messaging.BrokeredMessage message)
        {
            _baseClient.Send(message);
        }

        public Task SendAsync(Microsoft.ServiceBus.Messaging.BrokeredMessage message)
        {
            return _baseClient.SendAsync(message);
        }

        public void SendBatch(IEnumerable<BrokeredMessage> messages)
        {
            _baseClient.SendBatch(messages);
        }

        public Task SendBatchAsync(IEnumerable<BrokeredMessage> messages)
        {
            return _baseClient.SendBatchAsync(messages);
        }

        public IAsyncResult BeginSend(Microsoft.ServiceBus.Messaging.BrokeredMessage message, AsyncCallback callback, object state)
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

        public string Path
        {
            get { return _baseClient.Path; }
        }

        public Microsoft.ServiceBus.Messaging.MessagingFactory MessagingFactory
        {
            get { return _baseClient.MessagingFactory; }
        }

        private readonly Microsoft.ServiceBus.Messaging.TopicClient _baseClient;

        private TopicClient(Microsoft.ServiceBus.Messaging.TopicClient baseClient)
        {
            _baseClient = baseClient;
        }

        public static IMessageSender Create(string path)
        {
            return new TopicClient(Microsoft.ServiceBus.Messaging.TopicClient.Create(path));
        }

        public static IMessageSender CreateFromConnectionString(string connectionString, string path)
        {
            return new TopicClient(Microsoft.ServiceBus.Messaging.TopicClient.CreateFromConnectionString(connectionString,path));
        }
    }
}