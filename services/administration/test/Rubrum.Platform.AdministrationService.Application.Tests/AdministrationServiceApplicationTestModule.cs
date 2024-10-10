using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.AdministrationService;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<AdministrationServiceApplicationModule>]
[DependsOn<AdministrationServiceEntityFrameworkCoreTestModule>]
public class AdministrationServiceApplicationTestModule : AbpModule;
