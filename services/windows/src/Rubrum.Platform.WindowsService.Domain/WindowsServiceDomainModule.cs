using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.WindowsService;

[DependsOn<AbpDddDomainModule>]
public class WindowsServiceDomainModule : AbpModule;
