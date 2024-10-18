using Rubrum.Modularity;
using Rubrum.Platform.Hosting;
using Testcontainers.RabbitMq;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;
using Volo.Abp.Threading;

namespace Rubrum.BackgroundJobs;

[DependsOn<AbpEventBusRabbitMqModule>]
[DependsOn<PlatformHostingAspNetCoreModule>]
[DependsOn<RubrumBackgroundJobsDistributedModule>]
public class RubrumBackgroundJobsDistributedTestModule : AbpModule
{
    private readonly RabbitMqContainer _container = new RabbitMqBuilder()
        .Build();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpRabbitMqOptions>(options =>
        {
            options.Connections.Default.Uri = new Uri(_container.GetConnectionString());
        });

        Configure<AbpRabbitMqEventBusOptions>(options =>
        {
            options.ConnectionName = "Default";
            options.ClientName = "RubrumBackgroundJobsDistributedTest";
            options.ExchangeName = "Rubrum";
        });
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => _container.StartAsync());
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var job = context.ServiceProvider.GetRequiredService<IBackgroundJobManager>();

        var result = await job.EnqueueAsync(new TestAsyncJobArgs("42"));
        var result2 = await job.EnqueueAsync(new TestAsyncJobArgs("42"));
        var result3 = await job.EnqueueAsync(new TestAsyncJobArgs("42"));
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        AsyncHelper.RunSync(() => _container.DisposeAsync().AsTask());
    }
}
