using Microsoft.Extensions.DependencyInjection;
using Rubrum.EntityFrameworkCore;
using Rubrum.Modularity;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

[DependsOn<AbpEntityFrameworkCorePostgreSqlModule>]
[DependsOn<RubrumEntityFrameworkCoreModule>]
[DependsOn<MyProjectNameDomainModule>]
public class MyProjectNameEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MyProjectNameDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });
    }
}
