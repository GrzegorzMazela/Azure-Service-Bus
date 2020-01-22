using System;
using System.Threading.Tasks;

namespace ServiceBus.QueueReceiver
{
    class Program
    {
        private static string serviceBusConnectionString = "XXXX";
        private static string queueName = "queue-simple";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");
            var receiver = new AzureQueueReceiver(serviceBusConnectionString, queueName);
            Console.WriteLine("Before Register Message Handler");
            receiver.RegisterOnMessageHandlerAndReceiveMessages();
            Console.WriteLine("After Register Message Handler");
            Console.ReadKey();
        }
    }
}
