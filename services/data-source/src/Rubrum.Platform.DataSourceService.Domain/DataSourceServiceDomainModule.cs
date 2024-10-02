using Rubrum.Auditing;
using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.DataSourceService;

[DependsOn<AbpDddDomainModule>]
[DependsOn<RubrumAuditingContractsModule>]
public class DataSourceServiceDomainModule : AbpModule;
