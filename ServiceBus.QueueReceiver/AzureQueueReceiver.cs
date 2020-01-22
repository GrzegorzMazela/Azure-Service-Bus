using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBus.DataContract;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.QueueReceiver
{
    public class AzureQueueReceiver
    {
        private IQueueClient queueClient { get; set; }

        public AzureQueueReceiver(string serviceBusConnectionString, string queueName)
        {
            queueClient = new QueueClient(serviceBusConnectionString, queueName);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            QueueMessage queueMessage = JsonConvert.DeserializeObject<QueueMessage>(jsonString);

            Console.WriteLine($"Received message: Id: {queueMessage.Id}; MessageText: {queueMessage.MessageText}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
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