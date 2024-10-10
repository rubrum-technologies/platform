using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<RubrumGraphqlTestModule>]
[DependsOn<BlobStorageServiceApplicationModule>]
[DependsOn<BlobStorageServiceEntityFrameworkCoreTestModule>]
public class BlobStorageServiceApplicationTestModule : AbpModule;
