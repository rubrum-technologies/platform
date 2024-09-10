using Microsoft.EntityFrameworkCore;
using Rubrum.EntityFrameworkCore;
using Rubrum.Platform.BlobStorageService.Blobs;
using Volo.Abp.Data;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

[ConnectionStringName(BlobStorageServiceDbProperties.ConnectionStringName)]
public class BlobStorageServiceDbContext(DbContextOptions<BlobStorageServiceDbContext> options)
    : RubrumDbContext<BlobStorageServiceDbContext>(options)
{
    public DbSet<Blob> Blobs => Set<Blob>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBlobStorageService();
    }
}
