using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.TopicSenderFilter
{
    class AzureTopicFilterSender
    {
        private ITopicClient topicClient { get; set; }

        public AzureTopicFilterSender(string serviceBusConnectionString, string topicName)
        {
            topicClient = new TopicClient(serviceBusConnectionString, topicName);
        }

        public async Task SendMessagesAsync(object obj)
        {
            string messageBody = JsonConvert.SerializeObject(obj);
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            message.UserProperties.Add("ObjName", obj.GetType().Name);
            await topicClient.SendAsync(message);
        }
    }
}
