using Microsoft.EntityFrameworkCore;
using Rubrum.Platform.WindowsService.Windows;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static Rubrum.Platform.WindowsService.WindowsServiceDbProperties;

namespace Rubrum.Platform.WindowsService.EntityFrameworkCore;

public static class WindowsServiceDbContextModelCreatingExtensions
{
    public static void ConfigureWindowsService(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Window>(b =>
        {
            b.ToTable(DbTablePrefix + "Windows", DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .HasMaxLength(WindowsConstants.MaxNameLength)
                .IsRequired();

            b.Property(x => x.AppId)
                .IsRequired();

            b.OwnsOne(x => x.Position, bn =>
            {
                bn.Property(x => x.X)
                    .IsRequired();

                bn.Property(x => x.Y)
                    .IsRequired();
            });

            b.OwnsOne(x => x.Size, bn =>
            {
                bn.Property(x => x.Height)
                    .IsRequired();

                bn.Property(x => x.Width)
                    .IsRequired();
            });

            b.Property(x => x.AppId)
                .IsRequired();
        });
    }
}
