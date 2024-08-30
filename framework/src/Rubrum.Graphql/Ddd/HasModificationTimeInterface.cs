using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IHasModificationTime>]
public static partial class HasModificationTimeInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasModificationTime> descriptor)
    {
        descriptor.Name("HasModificationTime");
        descriptor.Description("A standard interface to add DeletionTime property to a class.");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.LastModificationTime)
            .Type<DateTimeType>();
    }
}
