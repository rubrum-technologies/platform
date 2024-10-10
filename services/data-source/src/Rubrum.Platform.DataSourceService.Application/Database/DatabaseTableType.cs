using HotChocolate.Types;
using Rubrum.Graphql;

namespace Rubrum.Platform.DataSourceService.Database;

[ObjectType<DatabaseTable>]
public static partial class DatabaseTableType
{
    static partial void Configure(IObjectTypeDescriptor<DatabaseTable> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Entity();

        descriptor
            .ImplementsNode()
            .IdField(x => x.Id);

        descriptor
            .Field(x => x.DatabaseSourceId)
            .ID<DataSource>();

        descriptor.Field(x => x.Name);
        descriptor.Field(x => x.SystemName);
        descriptor.Field(x => x.Columns);
    }
}
