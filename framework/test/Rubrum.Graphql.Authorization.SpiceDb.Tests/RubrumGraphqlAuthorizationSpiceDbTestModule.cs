using Microsoft.Extensions.DependencyInjection;
using Rubrum.Graphql;
using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Authorization.Analyzers;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<RubrumGraphqlAuthorizationSpiceDbModule>]
public class RubrumGraphqlAuthorizationSpiceDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddTestsTypes()
            .AddFakeAuthorizationHandler();
    }
}
