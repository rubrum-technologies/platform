using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

public class BlobStorageServiceDbContextFactory : IDesignTimeDbContextFactory<BlobStorageServiceDbContext>
{
    public BlobStorageServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<BlobStorageServiceDbContext>()
            .UseNpgsql(
                string.Empty,
                b =>
                {
                    b.MigrationsHistoryTable("__BlobStorageService_Migrations");
                });

        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new BlobStorageServiceDbContext(builder.Options);
    }
}
