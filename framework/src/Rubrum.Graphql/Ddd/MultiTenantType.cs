using HotChocolate.Types;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Graphql.Ddd;

[InterfaceType<IMultiTenant>]
public static partial class MultiTenantType
{
    static partial void Configure(IInterfaceTypeDescriptor<IMultiTenant> descriptor)
    {
        descriptor.Name("MultiTenant");

        descriptor.BindFieldsExplicitly();

        descriptor
            .Field(x => x.TenantId)
            .ID();
    }
}
