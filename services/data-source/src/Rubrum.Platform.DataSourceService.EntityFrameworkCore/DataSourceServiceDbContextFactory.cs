using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore;

public class DataSourceServiceDbContextFactory : IDesignTimeDbContextFactory<DataSourceServiceDbContext>
{
    public DataSourceServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DataSourceServiceDbContext>()
            .UseNpgsql(
                string.Empty,
                b =>
                {
                    b.MigrationsHistoryTable("__DataSourceService_Migrations");
                });

        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new DataSourceServiceDbContext(builder.Options);
    }
}
