using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql;

[InterfaceType<IFullAuditedObject>]
public static partial class FullAuditedObjectType
{
    static partial void Configure(IInterfaceTypeDescriptor<IFullAuditedObject> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("FullAuditedObject");
        descriptor.Description("This interface adds IDeletionAuditedObject to IAuditedObject.");
    }
}
