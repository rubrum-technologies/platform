using HotChocolate.Types;

namespace Rubrum.Platform.DataSourceService.Database;

[InterfaceType<DatabaseSource>]
public static partial class DatabaseSourceType
{
    static partial void Configure(IInterfaceTypeDescriptor<DatabaseSource> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Tables);
    }
}
