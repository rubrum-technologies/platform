using Rubrum.Graphql;
using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<RubrumGraphqlAuthorizationSpiceDbModule>]
[DependsOn<PlatformBlobStorageServiceApplicationModule>]
[DependsOn<PlatformBlobStorageServiceEntityFrameworkCoreTestModule>]
public class PlatformBlobStorageServiceApplicationTestModule : AbpModule;
