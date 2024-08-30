using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IHasDeletionTime>]
public static partial class HasDeletionTimeInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasDeletionTime> descriptor)
    {
        descriptor.Name("HasDeletionTime");
        descriptor.Description(
            "A standard interface to add DeletionTime property to a class. It also makes the class soft delete (see ISoftDelete).");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.DeletionTime)
            .Type<DateTimeType>();
    }
}
