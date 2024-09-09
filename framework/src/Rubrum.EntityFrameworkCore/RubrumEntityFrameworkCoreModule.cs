using Rubrum.Auditing;
using Rubrum.Modularity;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rubrum.EntityFrameworkCore;

[DependsOn<AbpEntityFrameworkCoreModule>]
[DependsOn<RubrumAuditingModule>]
public class RubrumEntityFrameworkCoreModule : AbpModule;
