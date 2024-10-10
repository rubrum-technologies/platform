using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpAspNetCoreMvcModule>]
[DependsOn<BlobStorageServiceApplicationContractsModule>]
public class BlobStorageServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(BlobStorageServiceHttpApiModule).Assembly);
        });
    }
}
