using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<PlatformBlobStorageServiceApplicationModule>]
[DependsOn<PlatformBlobStorageServiceEntityFrameworkCoreTestModule>]
public class PlatformBlobStorageServiceApplicationTestModule : AbpModule;
