using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn<StoreAppsServiceEntityFrameworkCoreTestModule>]
public class StoreAppsServiceDomainTestModule : AbpModule;
