using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<BlobStorageServiceEntityFrameworkCoreTestModule>]
public class BlobStorageServiceDomainTestModule : AbpModule;
