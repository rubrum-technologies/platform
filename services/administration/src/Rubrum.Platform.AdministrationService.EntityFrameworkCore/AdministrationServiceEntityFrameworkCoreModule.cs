using Microsoft.Extensions.DependencyInjection;
using Rubrum.EntityFrameworkCore;
using Rubrum.Modularity;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Rubrum.Platform.AdministrationService.EntityFrameworkCore;

[DependsOn<AbpEntityFrameworkCorePostgreSqlModule>]
[DependsOn<AbpPermissionManagementEntityFrameworkCoreModule>]
[DependsOn<RubrumEntityFrameworkCoreModule>]
[DependsOn<AdministrationServiceDomainModule>]
public class AdministrationServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<AdministrationServiceDbContext>(options =>
        {
            options
                .ReplaceDbContext<IPermissionManagementDbContext>()
                .AddDefaultRepositories();
        });
    }
}
