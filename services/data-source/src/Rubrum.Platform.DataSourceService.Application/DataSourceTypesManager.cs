using System.Runtime.Loader;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceTypesManager(
    IDataSourceRepository repository,
    IDataSourceCompiler compiler) : IDataSourceTypesManager, ISingletonDependency
{
    private readonly List<AssemblyLoadContext> _contexts = [];

    public IReadOnlyList<Type> GetTypes(DataSource source)
    {
        var context = _contexts.First(x => x.Name == source.Id.ToString());

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

            context.LoadFromStream(stream);

            _contexts.Add(context);
        }
    }

    public async Task ReCompilationAsync(DataSource source, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
