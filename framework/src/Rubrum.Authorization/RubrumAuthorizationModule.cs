using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Authorization;

[DependsOn<RubrumAuthorizationAbstractionsModule>]
public class RubrumAuthorizationModule : AbpModule;
