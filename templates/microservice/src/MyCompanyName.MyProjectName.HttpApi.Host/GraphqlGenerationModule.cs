using Rubrum.Graphql;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(RubrumGraphqlModule))]
internal class GraphqlGenerationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services
            .GetGraphql()
            .AddCoreTypes()
            //.AddApplicationTypes()
            .AddGraphQLServer();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseRouting();

        app.UseConfiguredEndpoints(endpoints => { endpoints.MapGraphQL(); });
    }
}
