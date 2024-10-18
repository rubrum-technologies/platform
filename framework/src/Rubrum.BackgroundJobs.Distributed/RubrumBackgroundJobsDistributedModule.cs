using Rubrum.Modularity;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.Modularity;

namespace Rubrum.BackgroundJobs;

[DependsOn<AbpBackgroundJobsAbstractionsModule>]
[DependsOn<AbpEventBusAbstractionsModule>]
public class RubrumBackgroundJobsDistributedModule : AbpModule;
