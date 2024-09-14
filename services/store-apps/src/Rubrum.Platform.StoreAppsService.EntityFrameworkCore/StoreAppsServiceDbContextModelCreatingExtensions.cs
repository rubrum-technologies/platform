using Microsoft.EntityFrameworkCore;
using Rubrum.Platform.StoreAppsService.Apps;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static Rubrum.Platform.StoreAppsService.StoreAppsServiceDbProperties;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

public static class StoreAppsServiceDbContextModelCreatingExtensions
{
    public static void ConfigureStoreAppsService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<App>(b =>
        {
            b.ToTable(DbTablePrefix + "Apps", DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired();

            b.OwnsOne(
                x => x.Version,
                a =>
                {
                    a.Property(x => x.Major)
                        .IsRequired();

                    a.Property(x => x.Minor)
                        .IsRequired();

                    a.Property(x => x.Patch)
                        .IsRequired();
                });
        });
    }
}
