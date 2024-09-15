using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpDddApplicationContractsModule>]
[DependsOn<PlatformBlobStorageServiceDomainSharedModule>]
public class PlatformBlobStorageServiceApplicationContractsModule : AbpModule;
