using Microsoft.EntityFrameworkCore;
using Rubrum.Platform.BlobStorageService.Blobs;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

public static class BlobStorageServiceDbContextModelCreatingExtensions
{
    public static void ConfigureBlobStorageService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Blob>(b =>
        {
            b.ConfigureByConvention();

        });
    }
}
