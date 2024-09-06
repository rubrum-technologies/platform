using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

public static class MayHaveCreatorExtensions
{
    public static IObjectTypeDescriptor<T> MustHaveCreator<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IMayHaveCreator
    {
        descriptor
            .Field(x => x.CreatorId)
            .ID("User");

        return descriptor;
    }
}
