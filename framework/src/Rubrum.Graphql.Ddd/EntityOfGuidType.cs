using HotChocolate.Types;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql;

[InterfaceType<IEntity<Guid>>]
public static partial class EntityOfGuidType
{
    static partial void Configure(IInterfaceTypeDescriptor<IEntity<Guid>> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("EntityOfGuid");
        descriptor.Description("Defines an entity with a single primary key with \"Id\" property.");

        descriptor
            .Field(x => x.Id)
            .Description("Unique identifier for this entity.")
            .ID();
    }
}
