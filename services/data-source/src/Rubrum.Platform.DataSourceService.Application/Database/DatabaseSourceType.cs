using HotChocolate.Types;
using Rubrum.Graphql;

namespace Rubrum.Platform.DataSourceService.Database;

[ObjectType<DatabaseSource>]
public static partial class DatabaseSourceType
{
    static partial void Configure(IObjectTypeDescriptor<DatabaseSource> descriptor)
    {
        descriptor.FullAuditedAggregateRoot();
        descriptor.MultiTenant();

        descriptor.Field(x => x.Kind);
        descriptor.Field(x => x.Tables);
    }
}
