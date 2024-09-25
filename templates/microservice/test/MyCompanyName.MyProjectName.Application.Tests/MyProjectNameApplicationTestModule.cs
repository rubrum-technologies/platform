using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<RubrumGraphqlFluentValidationModule>]
[DependsOn<MyProjectNameApplicationModule>]
[DependsOn<MyProjectNameEntityFrameworkCoreTestModule>]
public class MyProjectNameApplicationTestModule : AbpModule;
