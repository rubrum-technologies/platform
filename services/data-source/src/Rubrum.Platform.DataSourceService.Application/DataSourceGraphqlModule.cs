using System.Collections;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Graphql.Middlewares;
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
        var queryableAccessorFactory = serviceProvider.GetRequiredService<IDataSourceQueryableAccessorFactory>();
        var assemblyAccessorFactory = serviceProvider.GetRequiredService<IDataSourceAssemblyAccessorFactory>();
        var graphqlTypes = new List<ITypeSystemMember>();

        var sources = await repository.GetListAsync(true, cancellationToken);

        foreach (var source in sources)
        {
            var assemblyAccessor = assemblyAccessorFactory.Get(source);

            foreach (var entity in source.Entities)
            {
                var type = assemblyAccessor.GetType(entity);
                var name = $"{source.Prefix}{entity.Name}";
                var queryableAccessor = queryableAccessorFactory.Get(source);

                var constructor = typeof(ObjectTypeDynamic<>)
                    .MakeGenericType(type)
                    .GetConstructors()
                    .First(c => c.GetParameters().Length == 1);

                var obj = (ObjectType)constructor.Invoke([name]);

                graphqlTypes.Add(obj);

                graphqlTypes.Add(CreateQueryType(context, name, type, await queryableAccessor.GetAsync(entity)));
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
            .UseFiltering(type)
            .UseSorting(type);

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

    private sealed class ObjectTypeDynamic<T>(string name) : ObjectType<T>
    {
        protected override void Configure(IObjectTypeDescriptor<T> descriptor)
        {
            descriptor.Name(name);

            foreach (var property in typeof(T).GetProperties()
                         .Where(p =>
                             p.PropertyType.IsAssignableTo(typeof(IEnumerable)) &&
                             p.PropertyType.GenericTypeArguments.Length == 1))
            {
                var relationType = property.PropertyType.GenericTypeArguments[0];

                descriptor
                    .Field(property)
                    .UseFiltering(relationType)
                    .UseSorting(relationType);
            }
        }
    }
}
