using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Rubrum.TestContainers.PostgreSql;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.DataSourceService;

[DependsOn<AbpAutofacModule>]
[DependsOn<AbpTestBaseModule>]
[DependsOn<AbpAuthorizationModule>]
[DependsOn<AbpGuidsModule>]
[DependsOn<RubrumTestContainersPostgreSqlModule>]
[DependsOn<DataSourceServiceDomainModule>]
public class DataSourceServiceTestBaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
    }
}
