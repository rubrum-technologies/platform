using Volo.Abp;

namespace Rubrum.Platform.BlobStorageService.Folders;

public class FolderBlobNameAlreadyExistsException(Guid ownerId, Guid? parentId, string name) : BusinessException
{
    public Guid OwnerId => ownerId;

    public Guid? ParentId => parentId;

    public string Name => name;
}
