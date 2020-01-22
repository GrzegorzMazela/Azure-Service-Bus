using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.TopicSender
{
    public class AzureTopicSender
    {
        private ITopicClient topicClient { get; set; }

        public AzureTopicSender(string serviceBusConnectionString, string topicName)
        {
            topicClient = new TopicClient(serviceBusConnectionString, topicName);
        }

        public async Task SendMessagesAsync(object obj)
        {
            string messageBody = JsonConvert.SerializeObject(obj);
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await topicClient.SendAsync(message);
        }
    }
}