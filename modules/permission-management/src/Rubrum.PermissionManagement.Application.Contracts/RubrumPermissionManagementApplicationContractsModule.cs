using Rubrum.Modularity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Rubrum.PermissionManagement;

[DependsOn<AbpPermissionManagementApplicationContractsModule>]
public class RubrumPermissionManagementApplicationContractsModule : AbpModule;
