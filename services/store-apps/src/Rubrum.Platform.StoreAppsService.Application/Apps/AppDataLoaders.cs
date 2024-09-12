using GreenDonut;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace Rubrum.Platform.StoreAppsService.Apps;

public static class AppDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, App>> AppByIdAsync(
        IReadOnlyList<Guid> ids,
        IUnitOfWorkManager unitOfWorkManager,
        IAsyncQueryableExecuter asyncExecuter,
        IAppRepository repository,
        CancellationToken cancellationToken)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        var query = (await repository.GetQueryableAsync())
            .Where(x => ids.Contains(x.Id));

        var apps = await asyncExecuter.ToListAsync(query, cancellationToken);

        return apps.ToDictionary(x => x.Id, x => x);
    }
}
