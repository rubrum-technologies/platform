using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

public class MyProjectNameDbContextFactory : IDesignTimeDbContextFactory<MyProjectNameDbContext>
{
    public MyProjectNameDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyProjectNameDbContext>()
            .UseNpgsql(
                string.Empty,
                b =>
                {
                    b.MigrationsHistoryTable("__MyProjectName_Migrations");
                });

        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new MyProjectNameDbContext(builder.Options);
    }
}
