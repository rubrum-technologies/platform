using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpDddDomainSharedModule>]
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
    }
}
