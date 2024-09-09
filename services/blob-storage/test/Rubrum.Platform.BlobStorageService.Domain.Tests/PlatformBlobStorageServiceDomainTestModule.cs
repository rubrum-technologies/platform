using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<PlatformBlobStorageServiceEntityFrameworkCoreTestModule>]
public class PlatformBlobStorageServiceDomainTestModule : AbpModule;
