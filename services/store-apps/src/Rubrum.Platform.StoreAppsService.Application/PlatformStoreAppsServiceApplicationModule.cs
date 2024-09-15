using Microsoft.Extensions.DependencyInjection;
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
[DependsOn(typeof(PlatformStoreAppsServiceApplicationContractsModule))]
[DependsOn(typeof(PlatformStoreAppsServiceDomainModule))]
public class PlatformStoreAppsServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql
            .AddGlobalObjectIdentification()
            .AddApplicationTypes();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PlatformStoreAppsServiceApplicationModule>(validate: true);
        });
    }
}
