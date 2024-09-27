using HotChocolate.Types;
using Volo.Abp.Domain.Entities.Auditing;

namespace Rubrum.Graphql.Ddd;

public static class FullAuditedAggregateRootExtensions
{
    public static IObjectTypeDescriptor<T> FullAuditedAggregateRoot<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : FullAuditedAggregateRoot<Guid>
    {
        descriptor.AggregateRoot();
        descriptor.FullAudited();
        descriptor.ExtraProperties();

        return descriptor;
    }
}
