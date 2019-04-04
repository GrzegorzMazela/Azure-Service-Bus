using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Common.Receiver
{
    public class TopicReceiver : ITopicReceiver
    {
        private ISubscriptionClient subscriptionClient { get; set; }

        public TopicReceiver(ISubscriptionClient subscriptionClient)
        {
            this.subscriptionClient = subscriptionClient;
        }

        public void RegisterOnMessageHandlerAndReceiveMessages<T>(Action<T> action)
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            subscriptionClient.RegisterMessageHandler(
                async (message, token) =>
                {
                    try
                    {
                        var messageJson = Encoding.UTF8.GetString(message.Body);
                        var obj = JsonConvert.DeserializeObject<T>(messageJson);
                        action(obj);
                        await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    }
                    catch(Exception ex)
                    {
                        await subscriptionClient.AbandonAsync(message.SystemProperties.LockToken);
                    }
                }, messageHandlerOptions);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
