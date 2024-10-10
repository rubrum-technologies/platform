using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Rubrum.Platform.DataSourceService;

public abstract class DataSourceManager : DomainService
{
    protected IDataSourceRepository DataSourceRepository =>
        LazyServiceProvider.LazyGetRequiredService<IDataSourceRepository>();

    public async Task ChangeNameAsync(
        DataSource dataSource,
        string name,
        CancellationToken ct = default)
    {
        if (dataSource.Name == name)
        {
            return;
        }

        await CheckNameAsync(name, ct);

        dataSource.SetName(name);
    }

    public async Task ChangePrefixAsync(
        DataSource dataSource,
        string prefix,
        CancellationToken ct = default)
    {
        if (dataSource.Prefix == prefix)
        {
            return;
        }

        await CheckPrefixAsync(prefix, ct);

        dataSource.SetPrefix(prefix);
    }

    protected async Task CheckNameAsync(string name, CancellationToken ct = default)
    {
        if (await DataSourceRepository.AnyAsync(x => x.Name == name, ct))
        {
            throw new DataSourceNameAlreadyExistsException(name);
        }
    }

    protected async Task CheckPrefixAsync(string prefix, CancellationToken ct = default)
    {
        if (await DataSourceRepository.AnyAsync(x => x.Prefix == prefix, ct))
        {
            throw new DataSourcePrefixAlreadyExistsException(prefix);
        }
    }
}
