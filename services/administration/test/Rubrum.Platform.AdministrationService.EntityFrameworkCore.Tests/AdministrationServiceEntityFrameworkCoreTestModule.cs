using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Platform.AdministrationService.DbMigrations;
using Rubrum.Platform.AdministrationService.EntityFrameworkCore;
using Rubrum.Modularity;
using Rubrum.TestContainers.PostgreSql;
using Testcontainers.PostgreSql;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Rubrum.Platform.AdministrationService;

[DependsOn<RubrumTestContainersPostgreSqlModule>]
[DependsOn<AdministrationServiceTestBaseModule>]
[DependsOn<AdministrationServiceEntityFrameworkCoreModule>]
public class AdministrationServiceEntityFrameworkCoreTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var postgres = context.Services.GetSingletonInstance<PostgreSqlContainer>();

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(config =>
            {
                config.DbContextOptions.UseNpgsql(postgres.GetConnectionString());
            });
        });
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => context.ServiceProvider
            .GetRequiredService<EfCoreRuntimeDatabaseMigrator>()
            .CheckAndApplyDatabaseMigrationsAsync());
    }
}
