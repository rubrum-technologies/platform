using Volo.Abp;

namespace Rubrum.Platform.BlobStorageService.Folders;

public class FolderBlobParentHasDifferentOwnerException(Guid ownerId, Guid parentId) : BusinessException
{
    public Guid OwnerId => ownerId;

    public Guid ParentId => parentId;
}
