using Microsoft.Azure.ServiceBus;
using ServiceBus.Common.Receiver;
using ServiceBus.DataContract;
using System;

namespace ServiceBus.AuditsService
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Endpoint=sb://openskygmazela.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cGmtd62W3kpblyLcMA0Uu6bDe2P6LM3EPqzd117uWjw=";
            const string topicName = "licencetopic";
            const string subscriptionName = "audit";
            var subscriptionClient = new SubscriptionClient(connectionString, topicName, subscriptionName);
            var topicReceiver = new TopicReceiver(subscriptionClient);

            topicReceiver.RegisterOnMessageHandlerAndReceiveMessages<Licence>((licence) => 
            {
                Console.WriteLine($"Create new audit {licence.LicenceNumber}");
                Console.WriteLine($"For {licence.FirstName} {licence.LastName}");
                Console.WriteLine($"On {licence.CreateDateTime}");
                Console.WriteLine();
            });

            Console.WriteLine("Audit Start");

            Console.ReadKey();
            Console.WriteLine("End");
        }
    }
}
