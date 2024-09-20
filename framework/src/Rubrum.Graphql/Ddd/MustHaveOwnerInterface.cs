using HotChocolate.Types;
using Rubrum.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IMustHaveOwner>]
public static partial class MustHaveOwnerInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<IMustHaveOwner> descriptor)
    {
        descriptor.Name("MustHaveOwner");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.OwnerId)
            .ID();
    }
}
