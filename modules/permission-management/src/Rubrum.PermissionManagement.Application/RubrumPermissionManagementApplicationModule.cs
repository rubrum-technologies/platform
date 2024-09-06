using Rubrum.Modularity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Rubrum.PermissionManagement;

[DependsOn<AbpPermissionManagementApplicationModule>]
[DependsOn<RubrumPermissionManagementApplicationContractsModule>]
public class RubrumPermissionManagementApplicationModule : AbpModule;
