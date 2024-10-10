using Rubrum.Auditing;
using Rubrum.Modularity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService;

[DependsOn<AbpPermissionManagementDomainModule>]
[DependsOn<RubrumAuditingContractsModule>]
public class AdministrationServiceDomainModule : AbpModule;
