using Rubrum.Auditing;
using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpDddDomainModule>]
[DependsOn<RubrumAuditingContractsModule>]
public class MyProjectNameDomainModule : AbpModule;
