using System.Collections.Concurrent;

namespace WebApplication2.Services;

public class JobQueue<T>
{
    private readonly ConcurrentQueue<T> _jobs = new();
    private readonly SemaphoreSlim _signal = new(0);
    public int Count => _jobs.Count;

    public void Enqueue(T job)
    {
        _jobs.Enqueue(job);
        _signal.Release();
    }

    public async Task<T?> DequeueAsync(CancellationToken cancellationToken = default)
    {
        await _signal.WaitAsync(cancellationToken);
        _jobs.TryDequeue(out var job);
        return job;
    }
}