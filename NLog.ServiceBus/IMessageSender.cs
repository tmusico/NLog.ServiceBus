using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace NLog.ServiceBus
{
    internal interface IMessageSender
    {
        Task SendAsync(BrokeredMessage message);
        bool IsClosed { get;  }
        Task CloseAsync();
    }
}