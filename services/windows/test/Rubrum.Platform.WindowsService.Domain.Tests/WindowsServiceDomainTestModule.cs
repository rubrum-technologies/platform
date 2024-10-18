using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.WindowsService;

[DependsOn<WindowsServiceEntityFrameworkCoreTestModule>]
public class WindowsServiceDomainTestModule : AbpModule;
