using System;
using System.Threading.Tasks;

namespace ServiceBus.ConsoleAppSender
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string queueName = "appconsole";
            const string topicName = "testtopic";
            //var azureQueue = new AzureQueueSender(queueName);
            //var task = azureQueue.MainAsync();
            //task.Wait();


            var azureTopicSender = new AzureTopicSender(topicName);
            var azureTopicSenderTask = azureTopicSender.MainAsync();
            azureTopicSenderTask.Wait();
        }
    }
}