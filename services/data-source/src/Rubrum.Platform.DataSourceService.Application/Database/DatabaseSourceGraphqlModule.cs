using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using LinqToDB.Infrastructure;
using Rubrum.Graphql.Middlewares;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSourceGraphqlModule(
    IServiceProvider serviceProvider) : ITypeModule, ISingletonDependency
{
    public event EventHandler<EventArgs>? TypesChanged;

    public async ValueTask<IReadOnlyCollection<ITypeSystemMember>> CreateTypesAsync(
        IDescriptorContext context,
        CancellationToken cancellationToken)
    {
        var repository = serviceProvider.GetRequiredService<IDatabaseSourceRepository>();
        var typesManager = serviceProvider.GetRequiredService<IDataSourceTypesManager>();
        var queryableManager = serviceProvider.GetRequiredService<IDatabaseSourceQueryableManager>();
        var graphqlTypes = new List<ITypeSystemMember>();

        var sources = await repository.GetListAsync(true, cancellationToken);

        foreach (var source in sources)
        {
            foreach (var table in source.Tables)
            {
                var type = typesManager.GetType(table);
                var name = $"{source.Prefix}{table.Name}";
                var factoryQueryable = await queryableManager.GetFactoryQueryableAsync(table);

                graphqlTypes.Add(CreateQueryType(context, name, type, factoryQueryable));
            }
        }

        return graphqlTypes;
    }

    private static ObjectTypeExtension CreateQueryType(
        IDescriptorContext context,
        string name,
        Type type,
        Func<Task<IQueryable>> factoryQueryable)
    {
        var typeDefinition = new ObjectTypeDefinition("Query");

        var all = new ObjectFieldDefinition(
                $"{name}All".ToLowerFirstChar(),
                type: TypeReference.Parse($"[{name}!]!"),
                resolver: async _ => await factoryQueryable())
            .ToDescriptor(context)
            .UseProjection(type)
            .UseFiltering(type)
            .UseSorting(type)
            .ToDefinition();

        var firstDescriptor = new ObjectFieldDefinition(
                name.ToLowerFirstChar(),
                type: TypeReference.Parse(name),
                resolver: async _ => await factoryQueryable())
            .ToDescriptor(context);

        firstDescriptor.Extend().OnBeforeCreate((_, definition) => { definition.ResultType = type; });

        firstDescriptor
            .UseFirstOrDefault()
            .UseProjection(type)
            .UseFiltering(type);

        var firstDefinition = firstDescriptor.ToDefinition();

        var anyDescriptor = new ObjectFieldDefinition(
                $"{name}Any".ToLowerFirstChar(),
                type: TypeReference.Parse("Boolean!"),
                resolver: async _ => await factoryQueryable())
            .ToDescriptor(context);

        anyDescriptor.Extend().OnBeforeCreate((_, definition) => { definition.ResultType = type; });
        anyDescriptor
            .UseAny()
            .UseFiltering(type);

        var anyDefinition = anyDescriptor.ToDefinition();

        var countDescriptor = new ObjectFieldDefinition(
                $"{name}Count".ToLowerFirstChar(),
                type: TypeReference.Parse("Int!"),
                resolver: async _ => await factoryQueryable())
            .ToDescriptor(context);

        countDescriptor.Extend().OnBeforeCreate((_, definition) => { definition.ResultType = type; });
        countDescriptor
            .UseCount()
            .UseFiltering(type);

        var countDefinition = countDescriptor.ToDefinition();

        typeDefinition.Fields.Add(all);
        typeDefinition.Fields.Add(firstDefinition);
        typeDefinition.Fields.Add(anyDefinition);
        typeDefinition.Fields.Add(countDefinition);

        return ObjectTypeExtension.CreateUnsafe(typeDefinition);
    }
}
