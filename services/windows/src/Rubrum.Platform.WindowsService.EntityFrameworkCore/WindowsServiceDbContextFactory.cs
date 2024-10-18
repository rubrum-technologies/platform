using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rubrum.Platform.WindowsService.EntityFrameworkCore;

public class WindowsServiceDbContextFactory : IDesignTimeDbContextFactory<WindowsServiceDbContext>
{
    public WindowsServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<WindowsServiceDbContext>()
            .UseNpgsql(
                string.Empty,
                b => { b.MigrationsHistoryTable("__WindowsService_Migrations"); });

        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new WindowsServiceDbContext(builder.Options);
    }
}
