using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using NotificationGateway.Models;

namespace NotificationGateway.Services
{
    public class RabbitMQPublisher
    {
        private readonly string _hostName = "localhost";
        private readonly string _queueName;

        public RabbitMQPublisher(string queueName)
        {
            _queueName = queueName;
        }

        public void Publish(NotificationRequest request)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostName };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var message = JsonSerializer.Serialize(request);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing to RabbitMQ: {ex.Message}");
            }
        }
    }
}
