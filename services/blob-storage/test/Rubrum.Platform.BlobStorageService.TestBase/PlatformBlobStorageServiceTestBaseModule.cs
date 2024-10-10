using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpAutofacModule>]
[DependsOn<AbpTestBaseModule>]
[DependsOn<AbpAuthorizationModule>]
[DependsOn<AbpGuidsModule>]
[DependsOn<PlatformBlobStorageServiceDomainModule>]
public class PlatformBlobStorageServiceTestBaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
    }
}
