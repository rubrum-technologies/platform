using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

public static class StoreAppsServiceDbContextModelCreatingExtensions
{
    public static void ConfigureStoreAppsService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
