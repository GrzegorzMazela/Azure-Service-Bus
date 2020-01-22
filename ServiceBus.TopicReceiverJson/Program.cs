using ServiceBus.TopicReceiver;
using System;

namespace ServiceBus.TopicReceiverJson
{
    class Program
    {
        private static string serviceBusConnectionString = "XXXX";
        private static string topicName = "topic-test";
        private static string subscriptionName = "json";

        static void Main(string[] args)
        {
            Console.WriteLine("Start - Recevier JSON");
            AzureTopicReceiver receiver = new JsonConsoleLogAzureTopicReceiver(serviceBusConnectionString, topicName, subscriptionName);
            Console.WriteLine("Before Register Message Handler");
            receiver.RegisterOnMessageHandlerAndReceiveMessages();
            Console.WriteLine("After Register Message Handler");
            Console.ReadKey();
        }
    }
}
