using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<AbpAutofacModule>]
[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<RubrumGraphqlFluentValidationModule>]
public class RubrumGraphqlFluentValidationTestModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddMutationConventions()
            .AddFiltering()
            .AddSorting()
            .AddProjections();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql
            .AddTestsTypes()
            .AddFakeAuthorizationHandler();
    }
}
