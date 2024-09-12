using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(StoreAppsServiceApplicationContractsModule))]
public class StoreAppsServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(StoreAppsServiceHttpApiModule).Assembly);
        });
    }
}
