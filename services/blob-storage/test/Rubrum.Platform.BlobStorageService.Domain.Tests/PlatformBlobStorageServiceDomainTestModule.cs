using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn(typeof(PlatformBlobStorageServiceEntityFrameworkCoreTestModule))]
public class PlatformBlobStorageServiceDomainTestModule : AbpModule;
