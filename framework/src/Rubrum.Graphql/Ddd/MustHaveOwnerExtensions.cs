using HotChocolate.Types;
using Rubrum.Auditing;

namespace Rubrum.Graphql.Ddd;

public static class MustHaveOwnerExtensions
{
    public static IObjectTypeDescriptor<T> Owner<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IMustHaveOwner
    {
        descriptor
            .Field(x => x.OwnerId)
            .ID("User");

        return descriptor;
    }
}
