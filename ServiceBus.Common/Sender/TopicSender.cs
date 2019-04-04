using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Common.Sender
{
    public class TopicSender : ITopicSender
    {
        private ITopicClient topicClient { get; set; }

        public TopicSender(ITopicClient topicClient)
        {
            this.topicClient = topicClient;
        }

        public async Task SendMessagesAsync<T>(T obj)
        {
            string messageBody = JsonConvert.SerializeObject(obj);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await topicClient.SendAsync(message);
        }
    }
}