using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Rubrum.Graphql;

public static class RelayIdFieldExtensions
{
    // ReSharper disable once InconsistentNaming
    public static IObjectFieldDescriptor ID<T>(this IObjectFieldDescriptor descriptor)
    {
        descriptor.ID(typeof(T).Name);

        return descriptor;
    }

    public static string Format<T>(this INodeIdSerializer serializer, object internalId)
    {
        return serializer.Format(typeof(T).Name, internalId);
    }
}
