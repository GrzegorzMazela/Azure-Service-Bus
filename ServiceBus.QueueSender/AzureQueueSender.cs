using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.QueueSender
{
    public class AzureQueueSender
    {
        private IQueueClient queueClient { get; set; }

        public AzureQueueSender(string serviceBusConnectionString, string queueName)
        {
            queueClient = new QueueClient(serviceBusConnectionString, queueName);
        }

        public async Task SendMessage(object obj)
        {
            string messageBody = JsonConvert.SerializeObject(obj);
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await queueClient.SendAsync(message);
        }
    }
}