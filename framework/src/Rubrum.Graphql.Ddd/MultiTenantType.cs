using HotChocolate.Types;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Graphql;

[InterfaceType<IMultiTenant>]
public static partial class MultiTenantType
{
    static partial void Configure(IInterfaceTypeDescriptor<IMultiTenant> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Name("MultiTenant");

        descriptor
            .Field(x => x.TenantId)
            .Description("Id of the related tenant.")
            .ID();
    }
}
