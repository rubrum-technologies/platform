using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn(typeof(AbpAuthorizationAbstractionsModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(PlatformBlobStorageServiceDomainSharedModule))]
public class PlatformBlobStorageServiceApplicationContractsModule : AbpModule;
