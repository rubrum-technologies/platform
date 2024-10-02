using HotChocolate.Types;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql;

public static class EntityExtensions
{
    public static IObjectTypeDescriptor<T> Entity<T, TKey>(
        this IObjectTypeDescriptor<T> descriptor,
        string? typeName = default)
        where T : Entity<TKey>
    {
        descriptor.InternalEntity();

        if (string.IsNullOrWhiteSpace(typeName))
        {
            descriptor
                .Field(x => x.Id)
                .Description("Unique identifier for this entity.")
                .ID<T>();
        }
        else
        {
            descriptor
                .Field(x => x.Id)
                .Description("Unique identifier for this entity.")
                .ID(typeName);
        }

        return descriptor;
    }

    public static IObjectTypeDescriptor<T> Entity<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : Entity<Guid>
    {
        return descriptor.Entity<T, Guid>();
    }

    internal static IObjectTypeDescriptor<T> InternalEntity<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : Entity
    {
        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.GetKeys())
            .Name("keys")
            .Description("Returns an array of ordered keys for this entity.")
            .Type<NonNullType<ListType<AnyType>>>();

        return descriptor;
    }
}
