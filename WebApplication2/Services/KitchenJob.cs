using System.Text;
using RabbitMQ.Client;

namespace WebApplication2.Services;

public class KitchenJob
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "clients_queue", type: ExchangeType.Direct);

            const string routingKey = "client1";
            const string message = "Hello World!";
            
            channel.QueueDeclare(queue: routingKey,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            channel.BasicPublish(exchange: "clients_queue",
                routingKey: routingKey,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message));
        }, cancellationToken);
    }
}
