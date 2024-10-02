using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Rubrum.Platform.BlobStorageService.Folders;

public class FolderBlobManager(IFolderBlobRepository repository) : DomainService
{
    public async Task<FolderBlob> CreateAsync(
        Guid ownerId,
        Guid? parentId,
        string name,
        CancellationToken ct = default)
    {
        await CheckNameAsync(ownerId, parentId, name, ct);

        if (parentId is not null)
        {
            await CheckParentAsync(ownerId, parentId.Value, ct);
        }

        return new FolderBlob(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            ownerId,
            parentId,
            name);
    }

    public async Task ChangeParentIdAsync(
        FolderBlob folder,
        Guid? parentId,
        CancellationToken ct = default)
    {
        if (folder.ParentId == parentId)
        {
            return;
        }

        if (parentId is not null)
        {
            await CheckParentAsync(folder.OwnerId, parentId.Value, ct);
        }

        await CheckNameAsync(folder.OwnerId, parentId, folder.Name, ct);

        folder.ParentId = parentId;
    }

    public async Task ChangeNameAsync(
        FolderBlob folder,
        string name,
        CancellationToken ct = default)
    {
        if (folder.Name == name)
        {
            return;
        }

        await CheckNameAsync(folder.OwnerId, folder.ParentId, name, ct);

        folder.SetName(name);
    }

    private async Task CheckNameAsync(Guid ownerId, Guid? parentId, string name, CancellationToken ct = default)
    {
        if (await repository.AnyAsync(x => x.OwnerId == ownerId && x.ParentId == parentId && x.Name == name, ct))
        {
            throw new FolderBlobNameAlreadyExistsException(ownerId, parentId, name);
        }
    }

    private async Task CheckParentAsync(Guid ownerId, Guid parentId, CancellationToken ct = default)
    {
        if (!await repository.AnyAsync(x => x.Id == parentId && x.OwnerId == ownerId, ct))
        {
            throw new FolderBlobParentHasDifferentOwnerException(ownerId, parentId);
        }
    }
}
