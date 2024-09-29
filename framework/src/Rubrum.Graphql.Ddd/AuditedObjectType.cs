using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IAuditedObject>]
public static partial class AuditedObjectType
{
    static partial void Configure(IInterfaceTypeDescriptor<IAuditedObject> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("AuditedObject");
        descriptor.Description("This interface can be implemented to add standard auditing properties to a class.");
    }
}
