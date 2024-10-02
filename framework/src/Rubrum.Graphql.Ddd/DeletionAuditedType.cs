using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IDeletionAuditedObject>]
public static partial class DeletionAuditedType
{
    static partial void Configure(IInterfaceTypeDescriptor<IDeletionAuditedObject> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("DeletionAuditedObject");
        descriptor.Description(
            "This interface can be implemented to store deletion information (who delete and when deleted).");

        descriptor
            .Field(x => x.DeleterId)
            .Description("Id of the deleter user.")
            .ID();
    }
}
