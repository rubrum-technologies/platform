using GreenDonut;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace Rubrum.Platform.WindowsService.Windows;

public static class WindowDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, Window>> WindowByIdAsync(
        IReadOnlyList<Guid> ids,
        IUnitOfWorkManager unitOfWorkManager,
        IAsyncQueryableExecuter asyncExecutor,
        IWindowRepository repository,
        CancellationToken cancellationToken)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        var query = (await repository.GetQueryableAsync())
            .Where(x => ids.Contains(x.Id));

        var windows = await asyncExecutor.ToListAsync(query, cancellationToken);

        return windows.ToDictionary(x => x.Id, x => x);
    }
}
