using ServiceBus.TopicReceiver;
using System;
using System.Threading.Tasks;

namespace ServiceBus.TopicReceiverJson
{
    public class JsonConsoleLogAzureTopicReceiver : AzureTopicReceiver
    {
        public JsonConsoleLogAzureTopicReceiver(string serviceBusConnectionString, string topicName, string subscriptionName)
               : base(serviceBusConnectionString, topicName, subscriptionName)
        {
        }

        protected override Task ProcessMessage(string jsonString)
        {
            Console.WriteLine($"JSon: {jsonString}");
            return Task.CompletedTask;
        }
    }
}