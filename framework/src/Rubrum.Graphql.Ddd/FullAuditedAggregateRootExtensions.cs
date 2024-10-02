using HotChocolate.Types;
using Volo.Abp.Domain.Entities.Auditing;

namespace Rubrum.Graphql;

public static class FullAuditedAggregateRootExtensions
{
    public static IObjectTypeDescriptor<T> FullAuditedAggregateRoot<T, TKey>(this IObjectTypeDescriptor<T> descriptor)
        where T : FullAuditedAggregateRoot<TKey>
    {
        descriptor.AggregateRoot<T, TKey>();
        descriptor.FullAudited();
        descriptor.ExtraProperties();

        return descriptor;
    }

    public static IObjectTypeDescriptor<T> FullAuditedAggregateRoot<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : FullAuditedAggregateRoot<Guid>
    {
        return descriptor.FullAuditedAggregateRoot<T, Guid>();
    }
}
