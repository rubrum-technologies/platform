using Microsoft.Extensions.DependencyInjection;
using Rubrum.EntityFrameworkCore;
using Rubrum.Modularity;
using Rubrum.Platform.DataSourceService.Database;
using Rubrum.Platform.DataSourceService.EntityFrameworkCore.Repositories;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore;

[DependsOn<AbpEntityFrameworkCorePostgreSqlModule>]
[DependsOn<RubrumEntityFrameworkCoreModule>]
[DependsOn<DataSourceServiceDomainModule>]
public class DataSourceServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DataSourceServiceDbContext>(options =>
        {
            options
                .AddRepository<DataSource, EfCoreDataSourceRepository>()
                .AddRepository<DatabaseSource, EfCoreDatabaseSourceRepository>()
                .AddDefaultRepositories();
        });
    }
}
