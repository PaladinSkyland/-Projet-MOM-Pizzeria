using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;

namespace WebApplication2.Services;

public class DelivererJob
{
    private int OrderId { get; set; }
    
    public DelivererJob(int orderId)
    {
        OrderId = orderId;
    }
    public async Task ExecuteAsync(Deliverer deliverer, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        await using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        var order = await context.Orders
            .Include("Clerk")
            .Include("Customer")
            .Include("OrdersRows.Product")
            .SingleAsync(o => o.Id == OrderId, cancellationToken: cancellationToken);
        
        order.Deliverer = deliverer;
        order.OrderStatus = "Delivering";
        await context.SaveChangesAsync(cancellationToken);

        await Task.Run(() =>
        {
            RabbitClient.SendNotification("deliverers_exchange", $"deliverer{deliverer.Id}", $"You have been assigned delivery of order n° {order.Id}. The delivery should be made at this address: {order.Customer.Address}");
            RabbitClient.SendNotification("customers_exchange", $"customer{order.Customer.Id}", $"Your order n° {order.Id} is in delivery");
        }, cancellationToken);

        await Task.Delay(10000, cancellationToken);
        
        order.OrderStatus = "Delivered";
        await context.SaveChangesAsync(cancellationToken);
                
        await Task.Run(() =>
        {
            RabbitClient.SendNotification("customers_exchange", $"customer{order.Customer.Id}", $"Your order n° {order.Id} has been delivered");
            RabbitClient.SendNotification("clerks_exchange", $"clerk{order.Clerk.Id}", $"Order n° {order.Id} delivered");
        }, cancellationToken);

        await Task.Delay(10000, cancellationToken);
        
        order.OrderStatus = "Closed";
        await context.SaveChangesAsync(cancellationToken);

        await Task.Run(() =>
        {
            RabbitClient.SendNotification("deliverers_exchange", $"deliverer{deliverer.Id}", $"Payment of {order.Price * .1}€ received for the delivery of order n° {order.Id}");
            RabbitClient.SendNotification("clerks_exchange", $"clerk{order.Clerk.Id}", $"Order n° {order.Id} closed");
        }, cancellationToken);
    }
}
