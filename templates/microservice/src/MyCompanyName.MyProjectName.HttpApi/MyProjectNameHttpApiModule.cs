using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpAspNetCoreMvcModule>]
[DependsOn<MyProjectNameApplicationContractsModule>]
public class MyProjectNameHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MyProjectNameHttpApiModule).Assembly);
        });
    }
}
