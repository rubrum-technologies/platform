using HotChocolate.Types;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql.Ddd;

public static class EntityExtensions
{
    public static IObjectTypeDescriptor<T> Entity<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : Entity<Guid>
    {
        descriptor.Ignore(x => x.GetKeys());
        descriptor.Ignore(x => x.EntityEquals(default!));

        return descriptor;
    }
}
