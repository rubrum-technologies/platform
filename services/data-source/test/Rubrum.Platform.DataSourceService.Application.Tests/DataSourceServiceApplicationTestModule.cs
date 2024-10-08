using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.DataSourceService;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<DataSourceServiceApplicationModule>]
[DependsOn<DataSourceServiceEntityFrameworkCoreTestModule>]
public class DataSourceServiceApplicationTestModule : AbpModule;
