using GreenDonut;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace Rubrum.Platform.DataSourceService;

public static class DataSourceDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, DataSource>> DataSourceByIdAsync(
        IReadOnlyList<Guid> ids,
        IUnitOfWorkManager unitOfWorkManager,
        IAsyncQueryableExecuter asyncExecuter,
        IDataSourceRepository repository,
        CancellationToken cancellationToken)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        var query = (await repository.GetQueryableAsync())
            .Where(x => ids.Contains(x.Id));

        var sources = await asyncExecuter.ToListAsync(query, cancellationToken);

        return sources.ToDictionary(x => x.Id, x => x);
    }
}
