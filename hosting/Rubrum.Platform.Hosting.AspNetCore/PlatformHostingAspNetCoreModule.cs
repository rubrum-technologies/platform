using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Rubrum.SpiceDb;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;

namespace Rubrum.Platform.Hosting;

[DependsOn<AbpSwashbuckleModule>]
[DependsOn<AbpAspNetCoreSerilogModule>]
[DependsOn<RubrumSpiceDbDaprModule>]
[DependsOn<PlatformHostingModule>]
public class PlatformHostingAspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = true; });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = true; });

        context.Services.AddGrpc(options => { options.EnableDetailedErrors = true; });

        context.Services.AddHealthChecks();
    }
}
