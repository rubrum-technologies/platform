using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

[ConnectionStringName(StoreAppsServiceDbProperties.ConnectionStringName)]
public class StoreAppsServiceDbContext(DbContextOptions<StoreAppsServiceDbContext> options)
    : AbpDbContext<StoreAppsServiceDbContext>(options), IStoreAppsServiceDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureStoreAppsService();
    }
}
