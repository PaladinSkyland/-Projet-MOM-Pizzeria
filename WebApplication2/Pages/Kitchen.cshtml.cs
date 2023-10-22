using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using RabbitMQ.Client;

namespace WebApplication2.Pages;

public class Kitchen : PageModel
{
    public IList<Notification> Notifications { get; set; } = new List<Notification>();

    public void OnGet()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "clients_queue", type: ExchangeType.Direct);
        var queue = channel.QueueDeclare(queue: "client1",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.QueueBind(queue: queue.QueueName,
            exchange: "clients_queue",
            routingKey: "client1");
        var message = channel.BasicGet(queue.QueueName, true);
        while (message != null)
        {
            Notifications.Add(new Notification("now", Encoding.UTF8.GetString(message.Body.ToArray())));
            message = channel.BasicGet(queue.QueueName, true);
        }
    }
}

public class Notification
{
    public string Time { get; set; }
    public string Text { get; set; }

    public Notification(string time, string text)
    {
        Time = time;
        Text = text;
    }
}