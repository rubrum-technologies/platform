using Rubrum.Graphql;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.AdministrationService;

[DependsOn(typeof(RubrumGraphqlTestModule))]
[DependsOn(typeof(PlatformAdministrationServiceApplicationModule))]
[DependsOn(typeof(PlatformAdministrationServiceEntityFrameworkCoreTestModule))]
public class PlatformAdministrationServiceApplicationTestModule : AbpModule;
