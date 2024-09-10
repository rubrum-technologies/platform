using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(AbpAuthorizationAbstractionsModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(StoreAppsServiceDomainSharedModule))]
public class StoreAppsServiceApplicationContractsModule : AbpModule;
