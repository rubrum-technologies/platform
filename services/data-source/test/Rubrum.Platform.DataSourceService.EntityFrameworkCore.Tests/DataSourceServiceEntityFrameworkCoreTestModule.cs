using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Rubrum.Platform.DataSourceService.DbMigrations;
using Rubrum.Platform.DataSourceService.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Rubrum.Platform.DataSourceService;

[DependsOn<DataSourceServiceTestBaseModule>]
[DependsOn<DataSourceServiceEntityFrameworkCoreModule>]
public class DataSourceServiceEntityFrameworkCoreTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var postgres = context.Services.GetSingletonInstance<PostgreSqlContainer>();

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<DataSourceServiceDbContext>(config =>
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
