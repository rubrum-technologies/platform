using HotChocolate.Types;
using Volo.Abp;

namespace Rubrum.Graphql;

[InterfaceType<ISoftDelete>]
public static partial class SoftDeleteType
{
    static partial void Configure(IInterfaceTypeDescriptor<ISoftDelete> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("SoftDelete");
        descriptor.Description(
            " Used to standardize soft deleting entities. Soft-delete entities are not actually deleted, marked as IsDeleted = true in the database, but can not be retrieved to the application normally.");

        descriptor
            .Field(x => x.IsDeleted)
            .Description("Used to mark an Entity as 'Deleted'.")
            .Type<NonNullType<BooleanType>>();
    }
}
