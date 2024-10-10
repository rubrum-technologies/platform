using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Rubrum.Platform.BlobStorageService.DbMigrations;
using Rubrum.Platform.BlobStorageService.EntityFrameworkCore;
using Rubrum.TestContainers.PostgreSql;
using Testcontainers.PostgreSql;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<RubrumTestContainersPostgreSqlModule>]
[DependsOn<BlobStorageServiceTestBaseModule>]
[DependsOn<BlobStorageServiceEntityFrameworkCoreModule>]
public class BlobStorageServiceEntityFrameworkCoreTestModule : AbpModule
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
