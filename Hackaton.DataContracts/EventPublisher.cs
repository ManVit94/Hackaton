using System.Text;

using Newtonsoft.Json;
using RabbitMQ.Client;

using Hackaton.DataContracts.Messages;

namespace Hackaton.DataContracts
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IConnectionFactory _connectionFactory;

        public EventPublisher(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Publish(RequestCalledEvent @event)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hackaton",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            var json = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("", "hackaton", null, body);
        }
    }
}
