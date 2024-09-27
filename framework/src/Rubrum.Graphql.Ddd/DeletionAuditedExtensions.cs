using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

public static class DeletionAuditedExtensions
{
    public static IObjectTypeDescriptor<T> DeletionAudited<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IDeletionAuditedObject
    {
        descriptor
            .Field(x => x.DeleterId)
            .ID("User");

        return descriptor;
    }
}
