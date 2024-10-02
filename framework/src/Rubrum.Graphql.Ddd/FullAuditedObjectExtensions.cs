using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

public static class FullAuditedObjectExtensions
{
    public static IObjectTypeDescriptor<T> FullAudited<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IFullAuditedObject
    {
        descriptor.CreationAudited();
        descriptor.ModificationAudited();
        descriptor.DeletionAudited();

        return descriptor;
    }
}
