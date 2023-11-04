using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;

namespace WebApplication2.Services;

public class KitchenJob
{
    private int OrderId { get; set; }
    
    public KitchenJob(int orderId)
    {
        OrderId = orderId;
    }
    public async Task ExecuteAsync(JobQueue<DelivererJob> deliverersQueue, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        await using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        var order = await context.Orders
            .Include("Clerk")
            .Include("Customer")
            .SingleAsync(o => o.Id == OrderId, cancellationToken: cancellationToken);

        order.OrderStatus = "Preparing";
        await context.SaveChangesAsync(cancellationToken);
        
        await Task.Run(() =>
        {
            RabbitClient.SendNotification("clerks_exchange", $"clerk{order.Clerk.Id}", $"Order n° {order.Id} opened");
            RabbitClient.SendNotification("customers_exchange", $"customer{order.Customer.Id}", $"Your order n° {order.Id} is in preparation");
            RabbitClient.SendNotification("kitchen_exchange", "kitchen", $"New order received: n° {order.Id}");
        }, cancellationToken);

        await Task.Delay(10000, cancellationToken);
        
        order.OrderStatus = "Ready";
        await context.SaveChangesAsync(cancellationToken);

        await Task.Run(() =>
        {
            RabbitClient.SendNotification("customers_exchange", $"customer{order.Customer.Id}", $"Your order n° {order.Id} is ready");
        }, cancellationToken);
        
        deliverersQueue.Enqueue(new DelivererJob(order.Id));
    }
}
