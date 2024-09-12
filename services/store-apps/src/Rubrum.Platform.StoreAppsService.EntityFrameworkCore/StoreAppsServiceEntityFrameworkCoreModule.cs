using Microsoft.Extensions.DependencyInjection;
using Rubrum.Platform.StoreAppsService.Apps;
using Rubrum.Platform.StoreAppsService.EntityFrameworkCore.Repositories;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
[DependsOn(typeof(StoreAppsServiceDomainModule))]
public class StoreAppsServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        StoreAppsServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<StoreAppsServiceDbContext>(options =>
        {
            options
                .AddRepository<App, EfCoreAppRepository>()
                .AddDefaultRepositories();
        });
    }
}
