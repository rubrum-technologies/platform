using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Rubrum.BackgroundJobs;

public class DistributedBackgroundJobHandler<TArgs> : IDistributedEventHandler<JobEnqueuedEvent<TArgs>>, ITransientDependency
{
    public async Task HandleEventAsync(JobEnqueuedEvent<TArgs> eventData)
    {
        var jobArgs = eventData.Args;

        await Console.Out.WriteAsync("Handle");
        /*
        var context = new JobExecutionContext(
            scope.ServiceProvider,
            JobConfiguration.JobType,
            Serializer.Deserialize(ea.Body.ToArray(), typeof(TArgs))
        );

        try
        {
            await JobExecuter.ExecuteAsync(context);
            ChannelAccessor!.Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        }
        catch (BackgroundJobExecutionException)
        {
        }
        catch (Exception)
        {
        }*/
    }
}
