using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IDeletionAuditedObject>]
public static partial class DeletionAuditedInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<IDeletionAuditedObject> descriptor)
    {
        descriptor.Name("DeletionAuditedObject");
        descriptor.Description(
            "This interface can be implemented to store deletion information (who delete and when deleted).");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.DeleterId)
            .ID();
    }
}
