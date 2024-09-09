using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpAspNetCoreMvcModule>]
[DependsOn<RubrumGraphqlAuthorizationModule>]
[DependsOn<PlatformBlobStorageServiceApplicationModule>]
internal class GraphqlGenerationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services
            .GetGraphql()
            .AddGraphQLServer();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseRouting();

        app.UseConfiguredEndpoints(endpoints => { endpoints.MapGraphQL(); });
    }
}
