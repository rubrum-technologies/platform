using GreenDonut;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public static class BlobDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, Blob>> BlobByIdAsync(
        IReadOnlyList<Guid> ids,
        IUnitOfWorkManager unitOfWorkManager,
        IAsyncQueryableExecuter asyncExecuter,
        IBlobRepository repository,
        CancellationToken cancellationToken)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        var query = (await repository.GetQueryableAsync())
            .Where(x => ids.Contains(x.Id));

        var blobs = await asyncExecuter.ToListAsync(query, cancellationToken);

        return blobs.ToDictionary(x => x.Id, x => x);
    }
}
