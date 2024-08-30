using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Rubrum.Platform.AdministrationService;

[DependsOn(typeof(AbpFeatureManagementDomainSharedModule))]
[DependsOn(typeof(AbpSettingManagementDomainSharedModule))]
[DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
public class PlatformAdministrationServiceDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdministrationServiceGlobalFeatureConfigurator.Configure();
        AdministrationServiceModuleExtensionConfigurator.Configure();
    }
}
