using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<ICreationAuditedObject>]
public static partial class CreationAuditedInterface
{
    static partial void Configure(IInterfaceTypeDescriptor<ICreationAuditedObject> descriptor)
    {
        descriptor.Name("CreationAuditedObject");
        descriptor.Description(
            "This interface can be implemented to store creation information (who and when created).");

        descriptor.BindFieldsExplicitly();
    }
}
