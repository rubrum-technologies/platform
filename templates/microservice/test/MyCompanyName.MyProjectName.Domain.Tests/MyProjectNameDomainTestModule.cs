using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(MyProjectNameEntityFrameworkCoreTestModule))]
public class MyProjectNameDomainTestModule : AbpModule;
