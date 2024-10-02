using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IHasModificationTime>]
public static partial class HasModificationTimeType
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasModificationTime> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("HasModificationTime");
        descriptor.Description("A standard interface to add DeletionTime property to a class.");

        descriptor
            .Field(x => x.LastModificationTime)
            .Description("The last modified time for this entity.")
            .Type<DateTimeType>();
    }
}
