using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

public static class DeletionAuditedExtensions
{
    public static IObjectTypeDescriptor<T> DeletionAudited<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IDeletionAuditedObject
    {
        descriptor
            .Field(x => x.DeleterId)
            .Description("Id of the deleter user.")
            .ID("User");

        return descriptor;
    }
}
