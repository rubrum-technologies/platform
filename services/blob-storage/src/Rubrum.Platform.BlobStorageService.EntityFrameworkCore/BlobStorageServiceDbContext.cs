using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

[ConnectionStringName(BlobStorageServiceDbProperties.ConnectionStringName)]
public class BlobStorageServiceDbContext(DbContextOptions<BlobStorageServiceDbContext> options)
    : AbpDbContext<BlobStorageServiceDbContext>(options), IBlobStorageServiceDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBlobStorageService();
    }
}
