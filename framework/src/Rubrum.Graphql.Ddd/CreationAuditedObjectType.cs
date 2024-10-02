using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<ICreationAuditedObject>]
public static partial class CreationAuditedObjectType
{
    static partial void Configure(IInterfaceTypeDescriptor<ICreationAuditedObject> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("CreationAuditedObject");
        descriptor.Description(
            "This interface can be implemented to store creation information (who and when created).");
    }
}
