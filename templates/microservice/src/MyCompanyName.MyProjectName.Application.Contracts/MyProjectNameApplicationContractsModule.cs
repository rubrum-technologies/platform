using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(AbpAuthorizationAbstractionsModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(MyProjectNameDomainSharedModule))]
public class MyProjectNameApplicationContractsModule : AbpModule;
