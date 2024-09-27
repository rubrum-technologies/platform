using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.BackgroundJobs.RabbitMQ;

namespace Rubrum.BackgroundJobs;

// TODO: Реализовать BackgroundJobs на Dapr, через PubSub
[DependsOn(typeof(AbpBackgroundJobsAbstractionsModule))]
[DependsOn(typeof(AbpBackgroundJobsRabbitMqModule))]
    public class RubrumBackgroundJobsDaprModule : AbpModule;
