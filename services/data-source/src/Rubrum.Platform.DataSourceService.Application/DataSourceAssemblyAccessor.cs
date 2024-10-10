using System.Runtime.Loader;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceAssemblyAccessor : IDataSourceAssemblyAccessor
{
    private readonly Dictionary<Guid, Type> _types = [];

    public DataSourceAssemblyAccessor(AssemblyLoadContext context, DataSource dataSource)
    {
        var types = context.Assemblies.SelectMany(a => a.GetTypes()).ToList();

        foreach (var entity in dataSource.Entities)
        {
            var type = types.Single(t => t.Name == $"{dataSource.Prefix}{entity.Name}");

            _types.Add(entity.Id, type);
        }
    }

    public Type GetType(DataSourceEntity entity)
    {
        return _types[entity.Id];
    }
}
