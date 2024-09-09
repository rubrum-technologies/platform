using Rubrum.Modularity;
using Rubrum.SpiceDb;
using Volo.Abp.Modularity;

namespace Rubrum.Authorization;

[DependsOn<RubrumSpiceDbModule>]
[DependsOn<RubrumAuthorizationModule>]
public class RubrumAuthorizationSpiceDbModule : AbpModule;
