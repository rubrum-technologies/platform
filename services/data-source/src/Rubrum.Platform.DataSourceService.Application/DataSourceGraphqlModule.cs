using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Platform.DataSourceService.Database;
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
        var repository = serviceProvider.GetRequiredService<IDatabaseSourceRepository>();
        var typesManager = serviceProvider.GetRequiredService<IDataSourceTypesManager>();
        var graphqlTypes = new List<ITypeSystemMember>();
        var queryableManager = serviceProvider.GetRequiredService<IDatabaseSourceQueryableManager>();

        var sources = await repository.GetListAsync(true, cancellationToken);

        foreach (var source in sources)
        {
            foreach (var entity in source.Tables)
            {
                var type = typesManager.GetType(entity);

                var constructor = typeof(ObjectTypeDynamic<>)
                    .MakeGenericType(type)
                    .GetConstructors()
                    .First(c => c.GetParameters().Length == 1);

                var obj = (ObjectType)constructor.Invoke([$"{source.Prefix}{entity.Name}"]);

                graphqlTypes.Add(obj);

                var name = $"{source.Prefix}{entity.Name}";
                var factoryQueryable = await queryableManager.GetFactoryQueryableAsync(entity);

                graphqlTypes.Add(CreateQueryType(context, name, type, factoryQueryable));
            }
        }

        return graphqlTypes.AsReadOnly();
    }

    private static ObjectTypeExtension CreateQueryType(
        IDescriptorContext context,
        string name,
        Type type,
        Func<Task<IQueryable>> factoryQueryable)
    {
        var typeDefinition = new ObjectTypeDefinition("Query");

        var all = new ObjectFieldDefinition(
            $"{name}All",
            type: TypeReference.Parse($"[{name}!]!"),
            resolver: async _ => await factoryQueryable());

        all = all.ToDescriptor(context)
            .UseFiltering(type)
            .UseSorting(type)
            .ToDefinition();

        typeDefinition.Fields.Add(all);

        return ObjectTypeExtension.CreateUnsafe(typeDefinition);
    }

    private sealed class ObjectTypeDynamic<T>(string name)
        : ObjectType<T>(d => { d.Name(name); });
}
