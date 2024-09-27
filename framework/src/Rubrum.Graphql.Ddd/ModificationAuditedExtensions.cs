using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

public static class ModificationAuditedExtensions
{
    public static IObjectTypeDescriptor<T> ModificationAudited<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IModificationAuditedObject
    {
        descriptor
            .Field(x => x.LastModifierId)
            .ID("User");

        return descriptor;
    }
}
