using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.WindowsService;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<WindowsServiceApplicationModule>]
[DependsOn<WindowsServiceEntityFrameworkCoreTestModule>]
public class WindowsServiceApplicationTestModule : AbpModule;
