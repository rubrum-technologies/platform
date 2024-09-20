using Rubrum.Graphql;
using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<RubrumGraphqlAuthorizationSpiceDbModule>]
[DependsOn<RubrumGraphqlFluentValidationModule>]
[DependsOn<MyProjectNameApplicationModule>]
[DependsOn<MyProjectNameEntityFrameworkCoreTestModule>]
public class MyProjectNameApplicationTestModule : AbpModule;
