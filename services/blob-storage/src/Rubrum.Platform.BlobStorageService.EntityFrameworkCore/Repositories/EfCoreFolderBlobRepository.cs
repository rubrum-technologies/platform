using Rubrum.Platform.BlobStorageService.Folders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore.Repositories;

public class EfCoreFolderBlobRepository(IDbContextProvider<BlobStorageServiceDbContext> dbContextProvider)
    : EfCoreRepository<BlobStorageServiceDbContext, FolderBlob, Guid>(dbContextProvider), IFolderBlobRepository;
