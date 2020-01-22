using Newtonsoft.Json;

namespace ServiceBus.DataContract
{
    public class QueueMessage
    {
        [JsonConstructor]
        public QueueMessage(int id, string messageText)
        {
            Id = id;
            MessageText = messageText;
        }

        public int Id { get; protected set; }
        public string MessageText { get; protected set; }
    }
}