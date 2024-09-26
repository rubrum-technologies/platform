using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus.Distributed;

namespace Rubrum.BackgroundJobs;

public class DistributedBackgroundJobManager : IBackgroundJobManager
{
    private readonly IDistributedEventBus _distributedEventBus;

    public DistributedBackgroundJobManager(IDistributedEventBus distributedEventBus)
    {
        _distributedEventBus = distributedEventBus;
    }

    public async Task<string?> EnqueueAsync<TArgs>(
        TArgs args,
        BackgroundJobPriority priority = BackgroundJobPriority.Normal,
        TimeSpan? delay = null)
    {
        await _distributedEventBus.PublishAsync(new JobEnqueuedEvent<TArgs>
        {
            Args = args,
            Delay = delay,
            Priority = priority
        });

        return null;
    }
}
