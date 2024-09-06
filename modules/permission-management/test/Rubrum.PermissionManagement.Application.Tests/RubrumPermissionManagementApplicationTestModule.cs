using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rubrum.PermissionManagement;

[DependsOn<AbpAutofacModule>]
[DependsOn<AbpTestBaseModule>]
[DependsOn<AbpAuthorizationModule>]
[DependsOn<RubrumPermissionManagementApplicationModule>]
public class RubrumPermissionManagementApplicationTestModule : AbpModule;
