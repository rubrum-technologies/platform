using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

public static class FullAuditedExtensions
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
