using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.AdministrationService;

[DependsOn<AdministrationServiceEntityFrameworkCoreTestModule>]
public class AdministrationServiceDomainTestModule : AbpModule;
