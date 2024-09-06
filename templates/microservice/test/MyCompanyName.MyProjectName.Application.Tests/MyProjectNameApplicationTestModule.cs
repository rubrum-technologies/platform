using Rubrum.Graphql;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(RubrumGraphqlTestModule))]
[DependsOn(typeof(MyProjectNameApplicationModule))]
[DependsOn(typeof(MyProjectNameEntityFrameworkCoreTestModule))]
public class MyProjectNameApplicationTestModule : AbpModule;
