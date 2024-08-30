using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using Rubrum.Graphql;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

[DependsOn(typeof(RubrumGraphqlModule))]
[DependsOn(typeof(PlatformHostingAspNetCoreMicroserviceModule))]
public class PlatformHostingAspNetCoreMicroserviceGraphqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql
            .AddType(() => new TimeSpanType(TimeSpanFormat.DotNet))
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
