using Rubrum.Modularity;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace Rubrum.Auditing;

[DependsOn<AbpAuditingModule>]
[DependsOn<RubrumAuditingContractsModule>]
public class RubrumAuditingModule : AbpModule;
