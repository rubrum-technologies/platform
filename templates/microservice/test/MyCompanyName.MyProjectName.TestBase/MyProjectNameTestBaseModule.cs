using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpAutofacModule>]
[DependsOn<AbpTestBaseModule>]
[DependsOn<AbpAuthorizationModule>]
[DependsOn<AbpGuidsModule>]
[DependsOn<MyProjectNameDomainModule>]
public class MyProjectNameTestBaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
    }
}
