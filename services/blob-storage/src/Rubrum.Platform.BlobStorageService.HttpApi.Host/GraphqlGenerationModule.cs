using Rubrum.Graphql;
using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpAspNetCoreMvcModule>]
[DependsOn<RubrumGraphqlAuthorizationSpiceDbModule>]
[DependsOn<BlobStorageServiceApplicationModule>]
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
