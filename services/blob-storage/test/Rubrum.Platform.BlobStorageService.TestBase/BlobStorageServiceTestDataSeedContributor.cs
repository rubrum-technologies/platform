using Rubrum.Platform.BlobStorageService.Folders;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using static Rubrum.Platform.BlobStorageService.BlobStorageServiceTestConstants;

namespace Rubrum.Platform.BlobStorageService;

public class BlobStorageServiceTestDataSeedContributor(
    IUnitOfWorkManager unitOfWorkManager,
    IFolderBlobRepository folderBlobRepository) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        var folderBlob = new FolderBlob(FolderBlobId, null, Guid.Empty, null, "Test");

        await folderBlobRepository.InsertAsync(folderBlob);

        await uow.CompleteAsync();
    }
}
