using Microsoft.Extensions.DependencyInjection;
using Rubrum.EntityFrameworkCore;
using Rubrum.Modularity;
using Rubrum.Platform.WindowsService.EntityFrameworkCore.Repositories;
using Rubrum.Platform.WindowsService.Windows;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.WindowsService.EntityFrameworkCore;

[DependsOn<AbpEntityFrameworkCorePostgreSqlModule>]
[DependsOn<RubrumEntityFrameworkCoreModule>]
[DependsOn<WindowsServiceDomainModule>]
public class WindowsServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WindowsServiceDbContext>(options =>
        {
            options
                .AddRepository<Window, EfCoreWindowsRepository>()
                .AddDefaultRepositories();
        });
    }
}
