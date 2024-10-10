using HotChocolate.Types;

namespace Rubrum.Platform.DataSourceService;

[InterfaceType<DataSource>]
public static partial class DataSourceType
{
    static partial void Configure(IInterfaceTypeDescriptor<DataSource> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Name);
        descriptor.Field(x => x.Prefix);
        descriptor.Field(x => x.ConnectionString);
    }
}
