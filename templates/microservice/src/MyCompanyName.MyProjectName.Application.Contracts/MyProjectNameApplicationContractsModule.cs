using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpAuthorizationAbstractionsModule>]
[DependsOn<AbpDddApplicationContractsModule>]
[DependsOn<MyProjectNameDomainSharedModule>]
public class MyProjectNameApplicationContractsModule : AbpModule;
