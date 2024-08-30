using HotChocolate.Types;

namespace Rubrum.Graphql.Errors;

public static class AbpErrorExtensions
{
    public static IObjectFieldDescriptor UseAbpError(this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Error<AbpErrorFactory>();
    }
}
