using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

public class StoreAppsServiceDbContextFactory : IDesignTimeDbContextFactory<StoreAppsServiceDbContext>
{
    public StoreAppsServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<StoreAppsServiceDbContext>()
            .UseNpgsql(
                string.Empty,
                b =>
                {
                    b.MigrationsHistoryTable("__StoreAppsService_Migrations");
                });

        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new StoreAppsServiceDbContext(builder.Options);
    }
}
