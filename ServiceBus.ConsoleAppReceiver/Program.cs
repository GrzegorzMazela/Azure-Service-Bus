using System;
using System.Threading.Tasks;

namespace ServiceBus.ConsoleAppReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            const string queueName = "appconsole";
            const string topicName = "testtopic";
            //var azureQueue = new AzureQueueReceiver(queueName);
            //var task = azureQueue.MainAsync();
            //task.Wait();

            var azureTopicFirst = new AzureTopicReceiver(topicName, "first_reciver");
            var azureTopicSecond = new AzureTopicReceiver(topicName, "second_reciver");

            var azureTopicFirstTask = azureTopicFirst.MainAsync();
            var azureTopicSecondTask = azureTopicSecond.MainAsync();
            Console.ReadKey();
        }
    }
}
