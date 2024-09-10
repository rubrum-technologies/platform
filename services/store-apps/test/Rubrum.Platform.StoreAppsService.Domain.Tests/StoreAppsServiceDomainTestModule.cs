using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(StoreAppsServiceEntityFrameworkCoreTestModule))]
public class StoreAppsServiceDomainTestModule : AbpModule;
