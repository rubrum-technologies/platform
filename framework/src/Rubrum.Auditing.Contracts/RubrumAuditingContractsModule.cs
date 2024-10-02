using Rubrum.Modularity;
using Volo.Abp.Auditing;
using Volo.Abp.Modularity;

namespace Rubrum.Auditing;

[DependsOn<AbpAuditingContractsModule>]
public class RubrumAuditingContractsModule : AbpModule;
