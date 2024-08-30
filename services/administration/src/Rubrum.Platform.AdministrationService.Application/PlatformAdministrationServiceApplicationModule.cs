using Microsoft.Extensions.DependencyInjection;
using Rubrum.Graphql;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Rubrum.Platform.AdministrationService;

[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(RubrumGraphqlFluentValidationModule))]
[DependsOn(typeof(PlatformAdministrationServiceDomainModule))]
[DependsOn(typeof(PlatformAdministrationServiceApplicationContractsModule))]
public class PlatformAdministrationServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetGraphql();

        graphql.AddApplicationTypes();
    }
}
