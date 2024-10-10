using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn<AbpDddDomainModule>]
[DependsOn<PlatformStoreAppsServiceDomainSharedModule>]
public class PlatformStoreAppsServiceDomainModule : AbpModule;
