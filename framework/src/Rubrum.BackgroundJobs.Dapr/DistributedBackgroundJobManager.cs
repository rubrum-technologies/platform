using System.Collections;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;

namespace Rubrum.BackgroundJobs;

public class DistributedBackgroundJobManager : IBackgroundJobManager, ITransientDependency
{
    private readonly IDistributedEventBus _distributedEventBus;

    private ArrayList _handlers = new();

    public DistributedBackgroundJobManager(IDistributedEventBus distributedEventBus)
    {
        _distributedEventBus = distributedEventBus;
    }

    public async Task<string?> EnqueueAsync<TArgs>(
        TArgs args,
        BackgroundJobPriority priority = BackgroundJobPriority.Normal,
        TimeSpan? delay = null)
    {
        var handler = new DistributedBackgroundJobHandler<TArgs>();
        _handlers.Add(handler);

        _distributedEventBus.Subscribe(handler);

        var jobEnqueuedEvent = new JobEnqueuedEvent<TArgs>
        {
            Args = args,
            Delay = delay,
            Priority = priority
        };

        await _distributedEventBus.PublishAsync(jobEnqueuedEvent, false, false);

        return null;
    }
}
