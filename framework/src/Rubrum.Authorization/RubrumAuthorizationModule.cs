using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Authorization.Relations;
using Rubrum.Modularity;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Rubrum.Authorization;

[DependsOn<AbpCachingModule>]
[DependsOn<RubrumAuthorizationAbstractionsModule>]
public class RubrumAuthorizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AuthorizationOptions>(options =>
        {
            options.AddPolicy("RelationPolicy", p => p.Requirements.Add(new RelationRequirement()));
        });

        context.Services.AddSingleton<IAuthorizationHandler, RelationRequirementHandler>();
    }
}
