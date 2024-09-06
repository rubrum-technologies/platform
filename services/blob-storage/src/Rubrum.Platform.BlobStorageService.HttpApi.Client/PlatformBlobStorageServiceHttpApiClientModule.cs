using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn(typeof(AbpHttpClientModule))]
[DependsOn(typeof(PlatformBlobStorageServiceApplicationContractsModule))]
public class PlatformBlobStorageServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(PlatformBlobStorageServiceApplicationContractsModule).Assembly,
            BlobStorageServiceRemoteServiceConstants.RemoteServiceName);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PlatformBlobStorageServiceHttpApiClientModule>();
        });
    }
}
