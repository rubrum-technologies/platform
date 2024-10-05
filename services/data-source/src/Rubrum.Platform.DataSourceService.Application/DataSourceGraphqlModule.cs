using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceGraphqlModule(
    IServiceProvider serviceProvider) : ITypeModule, ISingletonDependency
{
    public event EventHandler<EventArgs>? TypesChanged;

    public async ValueTask<IReadOnlyCollection<ITypeSystemMember>> CreateTypesAsync(
        IDescriptorContext context,
        CancellationToken cancellationToken)
    {
        var repository = serviceProvider.GetRequiredService<IDataSourceRepository>();
        var typesManager = serviceProvider.GetRequiredService<IDataSourceTypesManager>();
        var graphqlTypes = new List<ITypeSystemMember>();

        var sources = await repository.GetListAsync(true, cancellationToken);

        foreach (var source in sources)
        {
            foreach (var entity in source.Entities)
            {
                var constructor = typeof(ObjectTypeDynamic<>)
                    .MakeGenericType(typesManager.GetType(entity))
                    .GetConstructors()
                    .First(c => c.GetParameters().Length == 1);

                var obj = (ObjectType)constructor.Invoke([$"{source.Prefix}{entity.Name}"]);

                graphqlTypes.Add(obj);
            }
        }

        return graphqlTypes.AsReadOnly();
    }

    private sealed class ObjectTypeDynamic<T>(string name)
        : ObjectType<T>(d => { d.Name(name); });
}
