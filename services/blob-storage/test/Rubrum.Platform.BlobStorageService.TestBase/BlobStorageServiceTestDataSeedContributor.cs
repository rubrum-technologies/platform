using Rubrum.Platform.BlobStorageService.Folders;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using static Rubrum.Platform.BlobStorageService.BlobStorageServiceTestConstants;

namespace Rubrum.Platform.BlobStorageService;

public class BlobStorageServiceTestDataSeedContributor(
    IFolderBlobRepository folderBlobRepository) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        var folderBlob = new FolderBlob(FolderBlobId, null, Guid.Empty, null, "Test");

        await folderBlobRepository.InsertAsync(folderBlob);
    }
}
