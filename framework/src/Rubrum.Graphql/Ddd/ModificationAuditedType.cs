using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IModificationAuditedObject>]
public static partial class ModificationAuditedType
{
    static partial void Configure(IInterfaceTypeDescriptor<IModificationAuditedObject> descriptor)
    {
        descriptor.Name("ModificationAuditedObject");
        descriptor.Description(
            "This interface can be implemented to store modification information (who and when modified lastly).");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.LastModifierId)
            .ID();
    }
}
