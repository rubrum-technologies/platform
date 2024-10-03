using Rubrum.Graphql;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(RubrumGraphqlTestModule))]
[DependsOn(typeof(PlatformStoreAppsServiceApplicationModule))]
[DependsOn(typeof(StoreAppsServiceEntityFrameworkCoreTestModule))]
public class StoreAppsServiceApplicationTestModule : AbpModule;
