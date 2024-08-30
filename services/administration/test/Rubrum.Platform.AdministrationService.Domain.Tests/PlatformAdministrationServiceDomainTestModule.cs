using Volo.Abp.Modularity;

namespace Rubrum.Platform.AdministrationService;

[DependsOn(typeof(PlatformAdministrationServiceEntityFrameworkCoreTestModule))]
public class PlatformAdministrationServiceDomainTestModule : AbpModule;
