using GreenDonut;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace Rubrum.Platform.BlobStorageService.Folders;

public static class FolderBlobDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, FolderBlob>> FolderBlobByIdAsync(
        IReadOnlyList<Guid> ids,
        IUnitOfWorkManager unitOfWorkManager,
        IAsyncQueryableExecuter asyncExecuter,
        IFolderBlobRepository repository,
        CancellationToken cancellationToken)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        var query = (await repository.GetQueryableAsync())
            .Where(x => ids.Contains(x.Id));

        var folders = await asyncExecuter.ToListAsync(query, cancellationToken);

        return folders.ToDictionary(x => x.Id, x => x);
    }
}
