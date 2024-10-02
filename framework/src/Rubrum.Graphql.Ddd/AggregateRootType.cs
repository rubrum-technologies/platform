using HotChocolate.Types;
using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql;

[InterfaceType<IAggregateRoot>]
public static partial class AggregateRootType
{
    static partial void Configure(IInterfaceTypeDescriptor<IAggregateRoot> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("AggregateRoot");
        descriptor.Description(
            "Defines an aggregate root. It's primary key may not be \"Id\" or it may have a composite primary key.");
    }
}
