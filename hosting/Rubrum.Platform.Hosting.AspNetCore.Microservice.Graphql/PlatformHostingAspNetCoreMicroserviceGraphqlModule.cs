using HotChocolate.Data.Filters;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using Rubrum.Graphql;
using Rubrum.Graphql.SpiceDb;
using Rubrum.Graphql.Subscriptions;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

[DependsOn<RubrumGraphqlDddModule>]
[DependsOn<RubrumGraphqlFluentValidationModule>]
[DependsOn<RubrumGraphqlAuthorizationSpiceDbModule>]
[DependsOn<RubrumGraphqlSubscriptionsDaprModule>]
[DependsOn<PlatformHostingAspNetCoreMicroserviceModule>]
public class PlatformHostingAspNetCoreMicroserviceGraphqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql
            .AddType(() => new TimeSpanType(TimeSpanFormat.DotNet))
            .AddConvention<IFilterConvention>(new FilterConventionExtension(descriptor =>
            {
                descriptor.BindRuntimeType<Guid, IdOperationFilterInputType>();
            }))
            .AddInstrumentation()
            .ModifyOptions(opt =>
            {
                opt.SortFieldsByName = true;
                opt.EnableDirectiveIntrospection = true;
                opt.EnableOneOf = true;
                opt.MaxAllowedNodeBatchSize = int.MaxValue;
            })
            .ModifyRequestOptions(options => { options.IncludeExceptionDetails = true; });

        context.Services
            .AddOpenTelemetry()
            .WithTracing(tracing => { tracing.AddHotChocolateInstrumentation(); });

        context.Services
            .GetGraphql()
            .AddGraphQLServer();
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql.InitializeOnStartup();
    }
}
