using HotChocolate.Types;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql.Ddd;

public static class AggregateRootExtensions
{
    public static IObjectTypeDescriptor<T> AggregateRoot<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : AggregateRoot<Guid>
    {
        descriptor.Entity();
        descriptor.Aggregate();
        descriptor.Ignore(x => x.GetLocalEvents());
        descriptor.Ignore(x => x.GetDistributedEvents());
        descriptor.Ignore(x => x.Validate(default!));

        return descriptor;
    }
}
