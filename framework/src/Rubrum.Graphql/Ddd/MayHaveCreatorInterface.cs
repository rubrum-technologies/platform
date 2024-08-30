using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IMayHaveCreator>]
public static partial class MayHaveCreatorInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<IMayHaveCreator> descriptor)
    {
        descriptor.Name("MayHaveCreator");
        descriptor.Description("Standard interface for an entity that MAY have a creator.");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.CreatorId)
            .ID();
    }
}
