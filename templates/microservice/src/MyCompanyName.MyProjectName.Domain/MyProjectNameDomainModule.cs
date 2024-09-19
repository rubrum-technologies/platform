using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpDddDomainModule>]
[DependsOn<MyProjectNameDomainSharedModule>]
public class MyProjectNameDomainModule : AbpModule;
