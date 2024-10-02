using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IMayHaveCreator>]
public static partial class MayHaveCreatorType
{
    static partial void Configure(IInterfaceTypeDescriptor<IMayHaveCreator> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("MayHaveCreator");
        descriptor.Description("Standard interface for an entity that MAY have a creator.");

        descriptor
            .Field(x => x.CreatorId)
            .Description("Id of the creator.")
            .ID();
    }
}
