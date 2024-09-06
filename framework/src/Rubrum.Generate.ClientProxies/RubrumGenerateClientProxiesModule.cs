using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Rubrum.Generate;

[DependsOn<RubrumGenerateModule>]
public class RubrumGenerateClientProxiesModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHostedService<ClientProxiesGenerateHostedService>();

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ExposeIntegrationServices = true;
        });
    }
}
