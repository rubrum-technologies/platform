using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Platform.BlobStorageService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn(typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(PlatformBlobStorageServiceApplicationContractsModule))]
public class PlatformBlobStorageServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PlatformBlobStorageServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BlobStorageServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
