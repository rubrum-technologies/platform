using Rubrum.Modularity;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpAutoMapperModule>]
[DependsOn<AbpDddDomainModule>]
[DependsOn<AbpBlobStoringModule>]
[DependsOn<PlatformBlobStorageServiceDomainSharedModule>]
public class PlatformBlobStorageServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PlatformBlobStorageServiceDomainModule>(validate: true);
        });
    }
}
