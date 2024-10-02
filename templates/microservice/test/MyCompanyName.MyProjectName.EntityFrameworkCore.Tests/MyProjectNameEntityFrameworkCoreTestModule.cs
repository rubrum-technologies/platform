using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.MyProjectName.DbMigrations;
using MyCompanyName.MyProjectName.EntityFrameworkCore;
using Rubrum.Modularity;
using Rubrum.TestContainers.PostgreSql;
using Testcontainers.PostgreSql;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace MyCompanyName.MyProjectName;

[DependsOn<RubrumTestContainersPostgreSqlModule>]
[DependsOn<MyProjectNameTestBaseModule>]
[DependsOn<MyProjectNameEntityFrameworkCoreModule>]
public class MyProjectNameEntityFrameworkCoreTestModule : AbpModule
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
