using Rubrum.Authorization;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql.SpiceDb;

[DependsOn<RubrumAuthorizationSpiceDbModule>]
[DependsOn<RubrumGraphqlAuthorizationModule>]
public class RubrumGraphqlAuthorizationSpiceDbModule : AbpModule;
