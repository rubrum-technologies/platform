using HotChocolate.Types;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql;

[InterfaceType<IEntity>]
public static partial class EntityType
{
    static partial void Configure(IInterfaceTypeDescriptor<IEntity> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("Entity");
        descriptor.Description(
            "Defines an entity. It's primary key may not be \"Id\" or it may have a composite primary key.");

        descriptor
            .Field(x => x.GetKeys())
            .Name("keys")
            .Description("Returns an array of ordered keys for this entity.")
            .Type<NonNullType<ListType<AnyType>>>();
    }
}
