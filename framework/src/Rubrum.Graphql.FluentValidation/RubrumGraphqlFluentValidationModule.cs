using Microsoft.Extensions.DependencyInjection;
using Rubrum.Graphql.Localization.Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.FluentValidation;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Rubrum.Graphql;

[DependsOn<AbpFluentValidationModule>]
[DependsOn<AbpLocalizationModule>]
[DependsOn<RubrumGraphqlModule>]
public class RubrumGraphqlFluentValidationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql.TryAddTypeInterceptor<FluentValidationTypeInterceptor>();

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RubrumGraphqlFluentValidationModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<RubrumGraphqlFluentValidationResource>("en")
                .AddVirtualJson("/Localization/Rubrum/Graphql/FluentValidation");

            options.DefaultResourceType = typeof(RubrumGraphqlFluentValidationResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Rubrum.Graphql", typeof(RubrumGraphqlFluentValidationResource));
        });
    }
}
