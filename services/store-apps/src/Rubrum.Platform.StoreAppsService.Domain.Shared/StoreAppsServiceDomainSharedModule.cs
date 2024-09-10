using Rubrum.Platform.StoreAppsService.Localization;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(AbpDddDomainSharedModule))]
public class StoreAppsServiceDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        StoreAppsServiceGlobalFeatureConfigurator.Configure();
        StoreAppsServiceModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<StoreAppsServiceDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<StoreAppsServiceResource>("ru")
                .AddVirtualJson("/Localization/Rubrum.Platform/StoreAppsService");

            options.DefaultResourceType = typeof(StoreAppsServiceResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Rubrum.Platform.StoreAppsService", typeof(StoreAppsServiceResource));
        });
    }
}
