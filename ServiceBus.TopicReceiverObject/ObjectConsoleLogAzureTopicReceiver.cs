using Newtonsoft.Json;
using ServiceBus.DataContract;
using ServiceBus.TopicReceiver;
using System;
using System.Threading.Tasks;

namespace ServiceBus.TopicReceiverObject
{
    public class ObjectConsoleLogAzureTopicReceiver : AzureTopicReceiver
    {
        public ObjectConsoleLogAzureTopicReceiver(string serviceBusConnectionString, string topicName, string subscriptionName)
            : base(serviceBusConnectionString, topicName, subscriptionName)
        {
        }

        protected override Task ProcessMessage(string jsonString)
        {
            QueueMessage queueMessage = JsonConvert.DeserializeObject<QueueMessage>(jsonString);
            Console.WriteLine($"Received message: Id: {queueMessage.Id}; MessageText: {queueMessage.MessageText}");
            return Task.CompletedTask;
        }
    }
}