using HotChocolate.Types;
using Rubrum.Graphql;

namespace Rubrum.Platform.DataSourceService.Database;

[ObjectType<DatabaseColumn>]
public static partial class DatabaseColumnType
{
    static partial void Configure(IObjectTypeDescriptor<DatabaseColumn> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Entity();

        descriptor
            .ImplementsNode()
            .IdField(x => x.Id);

        descriptor
            .Field(x => x.TableId)
            .ID<DatabaseTable>();

        descriptor.Field(x => x.Kind);
        descriptor.Field(x => x.Name);
        descriptor.Field(x => x.SystemName);
    }
}
