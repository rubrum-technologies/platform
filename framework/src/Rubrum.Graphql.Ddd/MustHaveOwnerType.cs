using HotChocolate.Types;
using Rubrum.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IMustHaveOwner>]
public static partial class MustHaveOwnerType
{
    static partial void Configure(IInterfaceTypeDescriptor<IMustHaveOwner> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("MustHaveOwner");

        descriptor
            .Field(x => x.OwnerId)
            .Description("Id of the owner.")
            .ID();
    }
}
