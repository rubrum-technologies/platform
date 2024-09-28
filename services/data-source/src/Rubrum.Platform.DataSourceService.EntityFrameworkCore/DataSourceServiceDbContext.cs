using Microsoft.EntityFrameworkCore;
using Rubrum.EntityFrameworkCore;
using Volo.Abp.Data;

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore;

[ConnectionStringName(DataSourceServiceDbProperties.ConnectionStringName)]
public class DataSourceServiceDbContext(DbContextOptions<DataSourceServiceDbContext> options)
    : RubrumDbContext<DataSourceServiceDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDataSourceService();
    }
}
