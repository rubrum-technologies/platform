using Volo.Abp.Domain.Services;
using Volo.Abp.Threading;

namespace Rubrum.Platform.BlobStorageService.Folders;

public class FolderBlobManager(
    IFolderBlobRepository repository,
    ICancellationTokenProvider cancellationTokenProvider) : DomainService
{
    public async Task<FolderBlob> CreateAsync(Guid? parentId, string name)
    {
        return new FolderBlob(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            parentId,
            name);
    }
}
