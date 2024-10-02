using Rubrum.Platform.BlobStorageService.Blobs;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore.Repositories;

public class EfCoreBlobRepository(IDbContextProvider<BlobStorageServiceDbContext> dbContextProvider)
    : EfCoreRepository<BlobStorageServiceDbContext, Blob, Guid>(dbContextProvider), IBlobRepository;
