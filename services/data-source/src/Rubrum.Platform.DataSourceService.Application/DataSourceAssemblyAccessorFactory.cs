using System.Collections.Concurrent;
using System.Runtime.Loader;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceAssemblyAccessorFactory(
    IDataSourceAssemblyCompiler assemblyCompiler) : IDataSourceAssemblyAccessorFactory, ITransientDependency
{
    private static readonly ConcurrentDictionary<Guid, IDataSourceAssemblyAccessor> Cache = [];

    public IDataSourceAssemblyAccessor Get(DataSource dataSource)
    {
        if (Cache.TryGetValue(dataSource.Id, out var manager))
        {
            return manager;
        }

        var context = Compilation(dataSource);

        manager = new DataSourceAssemblyAccessor(context, dataSource);

        Cache.AddOrUpdate(dataSource.Id, manager, (_, _) => manager);

        return manager;
    }

    private AssemblyLoadContext Compilation(DataSource dataSource)
    {
        if (!assemblyCompiler.TryCompile(dataSource, out var stream))
        {
            throw new DataSourceCompilationException();
        }

        var context = new AssemblyLoadContext(dataSource.Id.ToString(), true);

        context.LoadFromStream(stream);

        return context;
    }
}
