using HotChocolate.Types;
using Volo.Abp.Data;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IHasExtraProperties>]
public static partial class HasExtraPropertiesInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasExtraProperties> descriptor)
    {
        descriptor.Name("HasExtraProperties");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.ExtraProperties)
            .Type<JsonType>();
    }
}
