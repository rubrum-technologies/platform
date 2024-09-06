using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.VirtualFileSystem;

namespace Rubrum.PermissionManagement;

[DependsOn<AbpPermissionManagementHttpApiClientModule>]
[DependsOn<RubrumPermissionManagementApplicationContractsModule>]
public class RubrumPermissionManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(RubrumPermissionManagementApplicationContractsModule).Assembly,
            RubrumPermissionManagementRemoteServiceConstants.RemoteServiceName);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RubrumPermissionManagementHttpApiClientModule>();
        });
    }
}
