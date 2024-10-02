using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<RubrumGraphqlModule>]
public class RubrumGraphqlAspNetCoreModule : AbpModule;
