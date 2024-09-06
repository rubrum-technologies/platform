using HotChocolate.Types;
using Volo.Abp.Data;

namespace Rubrum.Graphql.Ddd;

public static class HasExtraPropertiesExtensions
{
    public static IObjectTypeDescriptor<T> ExtraProperties<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IHasExtraProperties
    {
        descriptor
            .Field(x => x.ExtraProperties)
            .Type<JsonType>();

        return descriptor;
    }
}
