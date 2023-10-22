namespace WebApplication2.Services;

public class KitchenService : BackgroundService
{
    private readonly JobQueue<KitchenJob> _queue;

    public KitchenService(JobQueue<KitchenJob> queue)
    {
        _queue = queue;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("starting...");
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("running...");
            var job = await _queue.DequeueAsync(stoppingToken);
            job?.ExecuteAsync(stoppingToken);
            await Task.Delay(5000, stoppingToken);
        }
    }

}
