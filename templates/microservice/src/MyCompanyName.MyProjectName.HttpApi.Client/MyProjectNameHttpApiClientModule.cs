using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(AbpHttpClientModule))]
[DependsOn(typeof(MyProjectNameApplicationContractsModule))]
public class MyProjectNameHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(MyProjectNameApplicationContractsModule).Assembly,
            MyProjectNameRemoteServiceConstants.RemoteServiceName);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyProjectNameHttpApiClientModule>();
        });
    }
}
