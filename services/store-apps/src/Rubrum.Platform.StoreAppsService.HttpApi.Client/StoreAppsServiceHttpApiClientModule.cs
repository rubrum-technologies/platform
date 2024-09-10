using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(AbpHttpClientModule))]
[DependsOn(typeof(StoreAppsServiceApplicationContractsModule))]
public class StoreAppsServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(StoreAppsServiceApplicationContractsModule).Assembly,
            StoreAppsServiceRemoteServiceConstants.RemoteServiceName);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<StoreAppsServiceHttpApiClientModule>();
        });
    }
}
