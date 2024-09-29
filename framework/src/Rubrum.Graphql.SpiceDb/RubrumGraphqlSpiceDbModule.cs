using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql.SpiceDb;

[DependsOn<RubrumGraphqlModule>]
public class RubrumGraphqlSpiceDbModule : AbpModule;
