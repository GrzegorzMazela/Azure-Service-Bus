using ServiceBus.DataContract;
using System;
using System.Threading.Tasks;

namespace ServiceBus.QueueSender
{
    class Program
    {
        private static string serviceBusConnectionString = "XXXX";
        private static string queueName = "queue-simple";

        static async Task Main(string[] args)
        { 
            Console.WriteLine("Start - Queue");
            var sender = new AzureQueueSender(serviceBusConnectionString, queueName);

            for(int i = 0; i < 10; i++)
            {
                var message = new QueueMessage(i, $"Message number {i}");
                await sender.SendMessage(message);
                Console.WriteLine($"Send {i}");
            }

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
