namespace WebApplication2.Services;

public class BackendService : BackgroundService
{
    private readonly ILogger<BackendService> _logger;
    private readonly JobQueue<BackendService> _queue;

    public BackendService(ILogger<BackendService> logger, JobQueue<BackendService> queue)
    {
        _logger = logger;
        _queue = queue;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var job = await _queue.DequeueAsync(stoppingToken);

            // do stuff
            _logger.LogInformation("Working on job {JobId}", stoppingToken);
            await Task.Delay(2000);
        }
    }

}
