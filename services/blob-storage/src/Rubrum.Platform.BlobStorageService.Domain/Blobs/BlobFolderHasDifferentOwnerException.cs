using Volo.Abp;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobFolderHasDifferentOwnerException(Guid ownerId, Guid folderId) : BusinessException
{
    public Guid OwnerId => ownerId;

    public Guid FolderId => folderId;
}

