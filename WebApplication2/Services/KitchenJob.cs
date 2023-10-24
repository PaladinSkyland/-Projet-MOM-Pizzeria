using WebApplication2.Models;

namespace WebApplication2.Services;

public class KitchenJob
{
    private Order Order { get; set; }
    public KitchenJob(Order order)
    {
        Order = order;
    }
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            RabbitClient.SendNotification("kitchen_exchange", "kitchen", $"New order: {Order.Id}");
            RabbitClient.SendNotification("customers_exchange", $"customer{Order.Customer.Id}", $"Your order n° ${Order.Id} is in preparation");
        }, cancellationToken);
    }
}
