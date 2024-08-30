using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn(typeof(RubrumGraphqlTestModule))]
[DependsOn(typeof(RubrumGraphqlFluentValidationModule))]
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
