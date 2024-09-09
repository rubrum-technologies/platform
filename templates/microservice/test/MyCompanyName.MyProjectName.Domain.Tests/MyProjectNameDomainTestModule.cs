using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<MyProjectNameEntityFrameworkCoreTestModule>]
public class MyProjectNameDomainTestModule : AbpModule;
