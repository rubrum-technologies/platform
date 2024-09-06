using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

public static class CreationAuditedExtensions
{
    public static IObjectTypeDescriptor<T> CreationAudited<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : ICreationAuditedObject
    {
        descriptor.MustHaveCreator();

        return descriptor;
    }
}
