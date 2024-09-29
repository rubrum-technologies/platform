using HotChocolate.Types;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql;

public static class AggregateRootExtensions
{
    public static IObjectTypeDescriptor<T> AggregateRoot<T, TKey>(this IObjectTypeDescriptor<T> descriptor)
        where T : AggregateRoot<TKey>
    {
        descriptor.Entity<T, TKey>();
        descriptor.Aggregate();

        return descriptor;
    }

    public static IObjectTypeDescriptor<T> AggregateRoot<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : AggregateRoot<Guid>
    {
        return descriptor.AggregateRoot<T, Guid>();
    }
}
