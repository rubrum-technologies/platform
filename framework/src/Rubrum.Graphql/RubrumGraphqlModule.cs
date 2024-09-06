using Microsoft.Extensions.DependencyInjection;
using Rubrum.Graphql.Interceptors;
using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<AbpDddApplicationModule>]
[DependsOn<AbpAuthorizationAbstractionsModule>]
[DependsOn<AbpExceptionHandlingModule>]
public class RubrumGraphqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.AddGraphQL();

        graphql
            .AddCoreTypes()
            .AddMutationConventions()
            .AddFiltering()
            .AddSorting()
            .AddProjections();

        context.Services.AddSingleton(graphql);
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql.TryAddTypeInterceptor<NewLineTypeInterceptor>();
    }
}
