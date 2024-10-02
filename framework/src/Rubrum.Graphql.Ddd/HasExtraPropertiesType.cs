using HotChocolate.Types;
using Volo.Abp.Data;

namespace Rubrum.Graphql;

[InterfaceType<IHasExtraProperties>]
public static partial class HasExtraPropertiesType
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasExtraProperties> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("HasExtraProperties");

        descriptor
            .Field(x => x.ExtraProperties)
            .Type<JsonType>();
    }
}
