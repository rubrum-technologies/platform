using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn<AbpDddDomainModule>]
public class StoreAppsServiceDomainModule : AbpModule;
