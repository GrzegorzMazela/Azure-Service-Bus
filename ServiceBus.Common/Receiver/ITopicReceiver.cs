using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBus.Common.Receiver
{
    public interface ITopicReceiver
    {
        void RegisterOnMessageHandlerAndReceiveMessages<T>(Action<T> action);
    }
}
