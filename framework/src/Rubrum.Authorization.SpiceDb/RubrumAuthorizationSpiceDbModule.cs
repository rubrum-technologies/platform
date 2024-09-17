using Rubrum.Authorization.Permissions;
using Rubrum.Authorization.Relations;
using Rubrum.Modularity;
using Rubrum.SpiceDb;
using Volo.Abp.Modularity;

namespace Rubrum.Authorization;

[DependsOn<RubrumSpiceDbModule>]
[DependsOn<RubrumAuthorizationModule>]
public class RubrumAuthorizationSpiceDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<RubrumPermissionOptions>(options =>
        {
            options.ValueProviders.Add<SpiceDbRelationValueProvider>();
        });
    }
}
