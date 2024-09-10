using Microsoft.EntityFrameworkCore;
using Rubrum.Platform.StoreAppsService.Apps;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

[ConnectionStringName(StoreAppsServiceDbProperties.ConnectionStringName)]
public class StoreAppsServiceDbContext(DbContextOptions<StoreAppsServiceDbContext> options)
    : AbpDbContext<StoreAppsServiceDbContext>(options), IStoreAppsServiceDbContext
{
    public DbSet<App> Apps => Set<App>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureStoreAppsService();
    }
}
