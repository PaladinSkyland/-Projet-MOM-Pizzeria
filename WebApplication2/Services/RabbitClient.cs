using System.Text;
using RabbitMQ.Client;

namespace WebApplication2.Services;

public static class RabbitClient
{
    public static void SendNotification(string exchange, string queue, string message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct);
        channel.QueueDeclare(queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
            
        channel.BasicPublish(exchange: exchange,
            routingKey: queue,
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(message));
    }

    public static IList<string> GetNotifications(string exchange, string queue)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct);
        channel.QueueDeclare(queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        channel.QueueBind(queue: queue,
            exchange: exchange,
            routingKey: queue);
        
        var message = channel.BasicGet(queue, true);
        var notifications = new List<string>();
        while (message != null)
        {
            notifications.Add(Encoding.UTF8.GetString(message.Body.ToArray()));
            message = channel.BasicGet(queue, true);
        }
        
        return notifications;
    }
}