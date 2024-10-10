using System.Reflection;
using LinqToDB;
using LinqToDB.Data;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSourceQueryableAccessor(
    DatabaseSource databaseSource,
    IDatabaseSourceDataOptionsAccessor dataOptionsAccessor,
    IDataSourceAssemblyAccessorFactory assemblyAccessorFactory) : IDataSourceQueryableAccessor
{
    protected static readonly MethodInfo GetTable = typeof(DataExtensions)
        .GetMethods()
        .First(x => x.Name == "GetTable" && x.GetParameters().Length == 1);

    private readonly DataOptions _dataOptions = dataOptionsAccessor.Get(databaseSource);
    private readonly IDataSourceAssemblyAccessor _assemblyAccessor = assemblyAccessorFactory.Get(databaseSource);

    public Task<Func<Task<IQueryable>>> GetAsync(DataSourceEntity entity)
    {
        var type = _assemblyAccessor.GetType(entity);

        return Task.FromResult(() => Factory(_dataOptions, type));

        static Task<IQueryable> Factory(DataOptions dataOptions, Type type)
        {
            var connection = new DataConnection(dataOptions);

            var queryable = GetTable.MakeGenericMethod(type).Invoke(null, [connection])!;

            return Task.FromResult((IQueryable)queryable);
        }
    }
}
