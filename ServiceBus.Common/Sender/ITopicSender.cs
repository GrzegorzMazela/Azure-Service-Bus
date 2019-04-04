using System.Threading.Tasks;

namespace ServiceBus.Common.Sender
{
    public interface ITopicSender
    {
        Task SendMessagesAsync<T>(T obj);
    }
}