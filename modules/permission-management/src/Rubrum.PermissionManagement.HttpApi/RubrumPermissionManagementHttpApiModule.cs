using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;

namespace Rubrum.PermissionManagement;

[DependsOn<AbpPermissionManagementHttpApiModule>]
[DependsOn<RubrumPermissionManagementApplicationContractsModule>]
public class RubrumPermissionManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(RubrumPermissionManagementHttpApiModule).Assembly);
        });
    }
}
