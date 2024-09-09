using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<MyProjectNameApplicationModule>]
[DependsOn<MyProjectNameEntityFrameworkCoreTestModule>]
public class MyProjectNameApplicationTestModule : AbpModule;
