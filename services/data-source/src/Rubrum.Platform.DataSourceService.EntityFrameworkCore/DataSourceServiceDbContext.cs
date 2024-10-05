using Microsoft.EntityFrameworkCore;
using Rubrum.EntityFrameworkCore;
using Rubrum.Platform.DataSourceService.Database;
using Volo.Abp.Data;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore;

[ConnectionStringName(DataSourceServiceDbProperties.ConnectionStringName)]
public class DataSourceServiceDbContext(DbContextOptions<DataSourceServiceDbContext> options)
    : RubrumDbContext<DataSourceServiceDbContext>(options)
{
    public DbSet<DataSource> DataSources => Set<DataSource>();

    public DbSet<DatabaseSource> DatabaseSources => Set<DatabaseSource>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureDataSourceService();

        base.OnModelCreating(modelBuilder);
    }
}
