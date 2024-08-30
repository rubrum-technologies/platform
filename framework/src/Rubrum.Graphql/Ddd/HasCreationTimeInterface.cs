using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IHasCreationTime>]
public static partial class HasCreationTimeInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<IHasCreationTime> descriptor)
    {
        descriptor.Name("HasCreationTime");
        descriptor.Description("A standard interface to add CreationTime property.");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.CreationTime)
            .Type<DateTimeType>();
    }
}
