using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(AbpDddDomainSharedModule))]
public class StoreAppsServiceDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        StoreAppsServiceGlobalFeatureConfigurator.Configure();
        StoreAppsServiceModuleExtensionConfigurator.Configure();
    }
}
