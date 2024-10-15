using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;

namespace Rubrum.BackgroundJobs;

// TODO: Реализовать BackgroundJobs на Dapr, через PubSub
[DependsOn(typeof(AbpBackgroundJobsAbstractionsModule))]
[DependsOn(typeof(AbpEventBusRabbitMqModule))]
public class RubrumBackgroundJobsDaprModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpRabbitMqOptions>(options =>
        {
            options.Connections.Default.Uri = new Uri("amqp://guest:guest@localhost:5672");
        });

        Configure<AbpRabbitMqEventBusOptions>(options =>
        {
            options.ConnectionName = "Default";
            options.ExchangeName = "Platform";
            options.ClientName = "TestApp1";
        });
    }
}
