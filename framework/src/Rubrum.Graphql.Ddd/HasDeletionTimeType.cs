using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IHasDeletionTime>]
public static partial class HasDeletionTimeType
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasDeletionTime> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("HasDeletionTime");
        descriptor.Description(
            "A standard interface to add DeletionTime property to a class. It also makes the class soft delete (see ISoftDelete).");

        descriptor
            .Field(x => x.DeletionTime)
            .Description("Deletion time.")
            .Type<DateTimeType>();
    }
}
