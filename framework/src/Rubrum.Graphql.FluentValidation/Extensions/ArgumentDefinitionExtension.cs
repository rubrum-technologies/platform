using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;

namespace Rubrum.Graphql.Extensions;

internal static class ArgumentDefinitionExtension
{
    public static Type? TryGetRuntimeType(this ArgumentDefinition argument)
    {
        if (argument.Parameter?.ParameterType is { } type)
        {
            return type;
        }

        if (argument.Type is ExtendedTypeReference extendedType)
        {
            return extendedType.Type.TryGetRuntimeType();
        }

        return null;
    }
}
