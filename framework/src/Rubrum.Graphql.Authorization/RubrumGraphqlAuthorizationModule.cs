using Microsoft.Extensions.DependencyInjection;
using Rubrum.Authorization;
using Rubrum.Graphql.Relations;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<RubrumGraphqlAspNetCoreModule>]
[DependsOn<RubrumAuthorizationAbstractionsModule>]
public class RubrumGraphqlAuthorizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddDirectiveType<RelationDirectiveType>()
            .AddDirectiveType<PermissionDirectiveType>()
            .AddAuthorization();
    }
}
