using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Rubrum.BackgroundJobs;

// TODO: Реализовать BackgroundJobs на Dapr, через PubSub
[DependsOn(typeof(AbpBackgroundJobsAbstractionsModule))]
public class RubrumBackgroundJobsDaprModule : AbpModule;
