using Microsoft.Azure.ServiceBus;
using ServiceBus.Common.Receiver;
using ServiceBus.DataContract;
using System;

namespace ServiceBus.LogsService
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "XXXX";
            const string topicName = "licencetopic";
            const string subscriptionName = "log";
            var subscriptionClient = new SubscriptionClient(connectionString, topicName, subscriptionName);
            var topicReceiver = new TopicReceiver(subscriptionClient);

            topicReceiver.RegisterOnMessageHandlerAndReceiveMessages<Licence>((licence) =>
            {
                Console.WriteLine($"Logs: Create {licence.LicenceNumber} - {licence.CreateDateTime}");
                Console.WriteLine();
            });

            Console.WriteLine("Log Start");

            Console.ReadKey();
            Console.WriteLine("End");
        }
    }
}
