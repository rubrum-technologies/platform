using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<AbpAuthorizationAbstractionsModule>]
[DependsOn<AbpDddApplicationContractsModule>]
[DependsOn<PlatformBlobStorageServiceDomainSharedModule>]
public class PlatformBlobStorageServiceApplicationContractsModule : AbpModule;
