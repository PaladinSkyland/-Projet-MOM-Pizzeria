using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;

namespace WebApplication2.Services;

public class DeliverersService : BackgroundService
{
    private readonly JobQueue<DelivererJob> _queue;
    private readonly IServiceProvider _serviceProvider;

    public DeliverersService(JobQueue<DelivererJob> queue, IServiceProvider serviceProvider)
    {
        _queue = queue;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var context = new ApplicationDbContext(_serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_queue.Count > 0)
            {
                var deliverer = await context.Deliverers.AsNoTracking().FirstOrDefaultAsync(
                    b => !b.Orders.Any(o => o.OrderStatus == "Delivering" || o.OrderStatus == "Delivered"),
                    cancellationToken: stoppingToken);
                if (deliverer is not null)
                {
                    var job = await _queue.DequeueAsync(stoppingToken);
                    job?.ExecuteAsync(deliverer, _serviceProvider, stoppingToken);
                }
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

}
