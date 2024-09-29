using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IModificationAuditedObject>]
public static partial class ModificationAuditedObjectType
{
    static partial void Configure(IInterfaceTypeDescriptor<IModificationAuditedObject> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("ModificationAuditedObject");
        descriptor.Description(
            "This interface can be implemented to store modification information (who and when modified lastly).");

        descriptor
            .Field(x => x.LastModifierId)
            .Description("Last modifier user for this entity.")
            .ID();
    }
}
