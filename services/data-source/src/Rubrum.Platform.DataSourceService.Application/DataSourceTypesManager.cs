using System.Runtime.Loader;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceTypesManager(
    IDataSourceRepository repository,
    IDataSourceCompiler compiler) : IDataSourceTypesManager, ISingletonDependency
{
    private readonly Dictionary<Guid, AssemblyLoadContext> _contexts = [];
    private readonly Dictionary<Guid, Type> _types = [];

    public Type GetType(DataSourceEntity entity)
    {
        return _types[entity.Id];
    }

    public IReadOnlyList<Type> GetTypes(DataSource source)
    {
        var context = _contexts[source.Id];

        return context.Assemblies.SelectMany(a => a.GetTypes()).ToList().AsReadOnly();
    }

    public async Task CompilationAsync(CancellationToken ct = default)
    {
        var sources = await repository.GetListAsync(true, ct);

        foreach (var source in sources)
        {
            if (!compiler.TryCompile(source, out var stream))
            {
                continue;
            }

            var context = new AssemblyLoadContext(source.Id.ToString(), true);

            var assembly = context.LoadFromStream(stream);
            var types = assembly.GetTypes();

            _contexts.Add(source.Id, context);

            foreach (var entity in source.Entities)
            {
                var type = types.Single(t => t.Name == $"{source.Prefix}{entity.Name}");

                _types.Add(entity.Id, type);
            }
        }
    }

    public async Task ReCompilationAsync(DataSource source, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
