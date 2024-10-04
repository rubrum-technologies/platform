using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using Microsoft.Extensions.DependencyInjection;
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
        var types = new List<ITypeSystemMember>();

        var sources = await repository.GetListAsync(true, cancellationToken);

        foreach (var source in sources)
        {
            var t = typesManager.GetTypes(source);

            foreach (var table in source.Tables)
            {
                var tt = t.First(z => z.Name == table.Name);

                var con = typeof(ObjectTypeTest<>).MakeGenericType(tt).GetConstructors()
                    .First(c => c.GetParameters().Length == 1);

                var obj = (ObjectType)con.Invoke([$"{source.Name}{table.Name}"]);

                types.Add(obj);
            }
        }

        return types.AsReadOnly();
    }

    private static ObjectTypeDefinition CreateObjectType(string prefix, DatabaseTable table, Type runtimeType)
    {
        var type = new ObjectTypeDefinition(prefix + table.Name, runtimeType: runtimeType);

        return type;
    }

    private static ObjectFieldDefinition CreateField(DatabaseColumn column)
    {
        var field = new ObjectFieldDefinition(
            column.Name,
            type: column.ToGraphqlType(),
            pureResolver: _ => "Test");

        return field;
    }
}

class ObjectTypeTest<T> : ObjectType<T>
{
    public ObjectTypeTest(string name)
        : base(d => { d.Name(name); })
    {
    }
}
