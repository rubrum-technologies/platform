using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;

namespace Rubrum.Platform.Hosting;

[DependsOn(typeof(AbpSwashbuckleModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(PlatformHostingModule))]
public class PlatformHostingAspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });

        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidate = true;
        });

        context.Services.AddHealthChecks();
    }
}
