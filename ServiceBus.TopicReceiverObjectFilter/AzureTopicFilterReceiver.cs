using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.TopicReceiverObjectFilter
{
    public class AzureTopicFilterReceiver
    {
        private ISubscriptionClient subscriptionClient { get; set; }

        public AzureTopicFilterReceiver(string serviceBusConnectionString, string topicName, string subscriptionName)
        {
            subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topicName, subscriptionName);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages<T>(Action<T> action)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            subscriptionClient.RegisterMessageHandler(async (message, token) =>
            {
                try
                {
                    var messageJson = Encoding.UTF8.GetString(message.Body);
                    var obj = JsonConvert.DeserializeObject<T>(messageJson);
                    action(obj);
                    await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                }
                catch (Exception ex)
                {
                    await subscriptionClient.AbandonAsync(message.SystemProperties.LockToken);
                }
            }, messageHandlerOptions);
        }

        private async Task AddFilters<T>()
        {
            var rules = await subscriptionClient.GetRulesAsync();
            if (!rules.Any(r => r. == "GoalsGreaterThanSeven"))
            {
                var filter = new Microsoft.Azure.ServiceBus.TrueFilter()
                await _subscriptionClient.AddRuleAsync("GoalsGreaterThanSeven", filter);
            }
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}