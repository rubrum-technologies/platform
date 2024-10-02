using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IHasCreationTime>]
public static partial class HasCreationTimeType
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasCreationTime> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("HasCreationTime");
        descriptor.Description("A standard interface to add CreationTime property.");

        descriptor
            .Field(x => x.CreationTime)
            .Description("Creation time.")
            .Type<DateTimeType>();
    }
}
