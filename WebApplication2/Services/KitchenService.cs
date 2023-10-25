namespace WebApplication2.Services;

public class KitchenService : BackgroundService
{
    private readonly JobQueue<KitchenJob> _kitchenQueue;
    private readonly JobQueue<DelivererJob> _deliverersQueue;
    private readonly IServiceProvider _serviceProvider;

    public KitchenService(JobQueue<KitchenJob> kitchenQueue, JobQueue<DelivererJob> deliverersQueue, IServiceProvider serviceProvider)
    {
        _kitchenQueue = kitchenQueue;
        _deliverersQueue = deliverersQueue;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var job = await _kitchenQueue.DequeueAsync(stoppingToken);
            job?.ExecuteAsync(_deliverersQueue, _serviceProvider, stoppingToken);
            await Task.Delay(100, stoppingToken);
        }
    }

}
