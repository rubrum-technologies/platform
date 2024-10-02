using Microsoft.Extensions.DependencyInjection;
using Rubrum.Auditing;
using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<AbpDddApplicationModule>]
[DependsOn<RubrumGraphqlModule>]
[DependsOn<RubrumAuditingContractsModule>]
public class RubrumGraphqlDddModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddDddTypes();
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql.TryAddTypeInterceptor<DtoTypeInterceptor>();
    }
}
