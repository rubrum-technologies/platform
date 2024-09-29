using HotChocolate.Types;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Graphql;

public static class MultiTenantExtensions
{
    public static IObjectTypeDescriptor<T> MultiTenant<T>(this IObjectTypeDescriptor<T> descriptor)
        where T : IMultiTenant
    {
        descriptor
            .Field(x => x.TenantId)
            .Description("Id of the related tenant.")
            .ID("Tenant");

        return descriptor;
    }
}
