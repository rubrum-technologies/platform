using Rubrum.Platform.BlobStorageService.Localization;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn(typeof(AbpDddDomainSharedModule))]
public class PlatformBlobStorageServiceDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        BlobStorageServiceGlobalFeatureConfigurator.Configure();
        BlobStorageServiceModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PlatformBlobStorageServiceDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<BlobStorageServiceResource>("ru")
                .AddVirtualJson("/Localization/Rubrum.Platform/BlobStorageService");

            options.DefaultResourceType = typeof(BlobStorageServiceResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Rubrum.Platform.BlobStorageService", typeof(BlobStorageServiceResource));
        });
    }
}
