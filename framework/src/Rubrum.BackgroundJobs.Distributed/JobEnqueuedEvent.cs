using Volo.Abp.BackgroundJobs;

namespace Rubrum.BackgroundJobs;

public class JobEnqueuedEvent<TArgs>
{
    public TArgs Args { get;  init; }

    public BackgroundJobPriority Priority { get; init; }

    public TimeSpan? Delay { get;  init; }
}
