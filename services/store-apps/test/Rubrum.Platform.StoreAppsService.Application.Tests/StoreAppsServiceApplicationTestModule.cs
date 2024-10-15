using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<StoreAppsServiceApplicationModule>]
[DependsOn<StoreAppsServiceEntityFrameworkCoreTestModule>]
public class StoreAppsServiceApplicationTestModule : AbpModule;
