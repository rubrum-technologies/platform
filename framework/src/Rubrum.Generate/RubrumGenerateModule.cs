using Microsoft.AspNetCore.Builder;
using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rubrum.Generate;

[DependsOn<AbpAutofacModule>]
[DependsOn<AbpAspNetCoreSerilogModule>]
[DependsOn<AbpAspNetCoreMvcModule>]
public class RubrumGenerateModule : AbpModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}
