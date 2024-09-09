using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<RubrumGraphqlFluentValidationModule>]
public class RubrumGraphqlFluentValidationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql
            .AddTestsTypes()
            .AddFakeAuthorizationHandler();
    }
}
