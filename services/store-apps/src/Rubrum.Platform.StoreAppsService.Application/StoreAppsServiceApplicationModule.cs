using Rubrum.Graphql;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(AbpFluentValidationModule))]
[DependsOn(typeof(AbpDddApplicationModule))]
[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(RubrumGraphqlModule))]
[DependsOn(typeof(StoreAppsServiceApplicationContractsModule))]
[DependsOn(typeof(StoreAppsServiceDomainModule))]
public class StoreAppsServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //var graphql = context.Services.GetGraphql();
        //graphql.AddApplicationTypes();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<StoreAppsServiceApplicationModule>(validate: true);
        });
    }
}
