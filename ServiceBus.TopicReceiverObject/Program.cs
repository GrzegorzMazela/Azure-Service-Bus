using ServiceBus.TopicReceiver;
using System;

namespace ServiceBus.TopicReceiverObject
{
    class Program
    {
        private static string serviceBusConnectionString = "XXXX";
        private static string topicName = "topic-test";
        private static string subscriptionName = "obj";

        static void Main(string[] args)
        {
            Console.WriteLine("Start - Recevier Object");
            AzureTopicReceiver receiver = new ObjectConsoleLogAzureTopicReceiver(serviceBusConnectionString, topicName, subscriptionName);
            Console.WriteLine("Before Register Message Handler");
            receiver.RegisterOnMessageHandlerAndReceiveMessages();
            Console.WriteLine("After Register Message Handler");
            Console.ReadKey();
        }
    }
}
