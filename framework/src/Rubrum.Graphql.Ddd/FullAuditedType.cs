using HotChocolate.Types;
using Volo.Abp.Auditing;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IFullAuditedObject>]
public static partial class FullAuditedType
{
    static partial void Configure(IInterfaceTypeDescriptor<IFullAuditedObject> descriptor)
    {
        descriptor.Name("FullAuditedObject");
        descriptor.Description("This interface adds IDeletionAuditedObject to IAuditedObject.");

        descriptor.BindFieldsExplicitly();
    }
}
