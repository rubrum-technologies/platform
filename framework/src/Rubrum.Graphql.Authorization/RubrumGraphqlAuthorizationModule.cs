using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<RubrumGraphqlAspNetCoreModule>]
public class RubrumGraphqlAuthorizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddAuthorization();
    }
}
