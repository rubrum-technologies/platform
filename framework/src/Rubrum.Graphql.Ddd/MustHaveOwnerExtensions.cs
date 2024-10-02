using HotChocolate.Types;
using Rubrum.Auditing;

namespace Rubrum.Graphql;

public static class MustHaveOwnerExtensions
{
    public static IObjectTypeDescriptor<T> Owner<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IMustHaveOwner
    {
        descriptor
            .Field(x => x.OwnerId)
            .Description("Id of the owner.")
            .ID("User");

        return descriptor;
    }
}
