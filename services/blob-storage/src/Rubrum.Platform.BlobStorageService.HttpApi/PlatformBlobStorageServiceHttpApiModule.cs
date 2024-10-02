using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpAspNetCoreMvcModule>]
[DependsOn<PlatformBlobStorageServiceApplicationContractsModule>]
public class PlatformBlobStorageServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PlatformBlobStorageServiceHttpApiModule).Assembly);
        });
    }
}
