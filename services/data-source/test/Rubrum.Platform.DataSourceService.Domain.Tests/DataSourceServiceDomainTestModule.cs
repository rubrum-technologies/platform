using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.DataSourceService;

[DependsOn<DataSourceServiceEntityFrameworkCoreTestModule>]
public class DataSourceServiceDomainTestModule : AbpModule;
