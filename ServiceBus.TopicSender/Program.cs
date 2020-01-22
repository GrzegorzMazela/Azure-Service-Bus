using ServiceBus.DataContract;
using System;
using System.Threading.Tasks;

namespace ServiceBus.TopicSender
{
    class Program
    {
        private static string serviceBusConnectionString = "XXXX";
        private static string topicName = "topic-test";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Start - Topic");
            var sender = new AzureTopicSender(serviceBusConnectionString, topicName);

            for (int i = 0; i < 10; i++)
            {
                var message = new QueueMessage(i, $"Message number {i}");
                await sender.SendMessagesAsync(message);
                Console.WriteLine($"Send {i}");
            }

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
