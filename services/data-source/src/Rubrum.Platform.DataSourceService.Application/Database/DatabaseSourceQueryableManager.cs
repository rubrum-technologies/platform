using System.Reflection;
using LinqToDB;
using LinqToDB.Data;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSourceQueryableManager(
    IDataSourceTypesManager typesManager,
    IDatabaseSourceDataOptionsBuilder dataOptionsBuilder,
    IDatabaseSourceRepository repository) : IDatabaseSourceQueryableManager, ISingletonDependency
{
    protected static readonly MethodInfo GetTable = typeof(DataExtensions)
        .GetMethod("GetTable")!;

    private readonly Dictionary<Guid, DataOptions> _options = [];

    public Task<Func<Task<IQueryable>>> GetFactoryQueryableAsync(DatabaseTable table)
    {
        var type = typesManager.GetType(table);
        var options = _options[table.DatabaseSourceId];

        return Task.FromResult<Func<Task<IQueryable>>>(() =>
        {
            var connection = new DataConnection(options);

            var queryable = GetTable.MakeGenericMethod(type).Invoke(null, [connection])!;

            return Task.FromResult((IQueryable)queryable);
        });
    }

    public async Task BuildAsync()
    {
        var sources = await repository.GetListAsync(true);

        foreach (var source in sources)
        {
            var dataOptions = await dataOptionsBuilder.BuildAsync(source);

            _options.Add(source.Id, dataOptions);
        }
    }
}
