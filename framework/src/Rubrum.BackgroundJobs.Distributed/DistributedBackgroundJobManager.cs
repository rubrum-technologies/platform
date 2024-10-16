using System.Collections;
using System.Reflection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Collections;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Rubrum.BackgroundJobs;

#pragma warning disable S3011

public class DistributedBackgroundJobManager(
    IDistributedEventBus distributedEventBus) : IBackgroundJobManager, ITransientDependency
{
    private static readonly MethodInfo SubscribeMethod = typeof(DistributedBackgroundJobManager)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
        .First(x => x.Name == nameof(Subscribe));

    private readonly TypeList _typesArgs = [];

    public async Task<string> EnqueueAsync<TArgs>(
        TArgs args,
        BackgroundJobPriority priority = BackgroundJobPriority.Normal,
        TimeSpan? delay = null)
    {
        if (!typeof(TArgs).IsClass)
        {
            throw new Exception(); // TODO: Сделать свой Exception
        }

        SubscribeMethod
            .MakeGenericMethod(typeof(TArgs))
            .Invoke(this, []);

        var jobEnqueuedEvent = new JobEnqueuedEvent<TArgs>
        {
            Args = args,
            Delay = delay,
            Priority = priority,
        };

        await distributedEventBus.PublishAsync(jobEnqueuedEvent, false, false);

        return string.Empty;
    }

    private void Subscribe<TArgs>()
        where TArgs : class
    {
        if (_typesArgs.Contains<TArgs>())
        {
            return;
        }

        distributedEventBus.Subscribe<JobEnqueuedEvent<TArgs>>(async _ =>
        {
            // TODO: Реализовать запуск JobHandler через JobExecutor
            Console.WriteLine("Handle Subscribe");
        });

        _typesArgs.Add<TArgs>();
    }
}
