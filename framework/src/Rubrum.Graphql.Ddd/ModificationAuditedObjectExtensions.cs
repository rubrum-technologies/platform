using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

public static class ModificationAuditedObjectExtensions
{
    public static IObjectTypeDescriptor<T> ModificationAudited<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IModificationAuditedObject
    {
        descriptor
            .Field(x => x.LastModifierId)
            .Description("Last modifier user for this entity.")
            .ID("User");

        return descriptor;
    }
}
