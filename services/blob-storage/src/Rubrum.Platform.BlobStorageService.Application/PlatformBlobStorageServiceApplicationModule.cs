using Microsoft.Extensions.DependencyInjection;
using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpFluentValidationModule>]
[DependsOn<AbpDddApplicationModule>]
[DependsOn<AbpAutoMapperModule>]
[DependsOn<RubrumGraphqlAuthorizationModule>]
[DependsOn<PlatformBlobStorageServiceApplicationContractsModule>]
[DependsOn<PlatformBlobStorageServiceDomainModule>]
public class PlatformBlobStorageServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql
            .AddGlobalObjectIdentification()
            .AddApplicationTypes();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PlatformBlobStorageServiceApplicationModule>(validate: true);
        });
    }
}
