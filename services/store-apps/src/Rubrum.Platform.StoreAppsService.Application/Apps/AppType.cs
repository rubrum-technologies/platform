using HotChocolate.Types;
using Rubrum.Graphql.Ddd;

namespace Rubrum.Platform.StoreAppsService.Apps;

[ObjectType<App>]
public static partial class AppType
{
    static partial void Configure(IObjectTypeDescriptor<App> descriptor)
    {
        descriptor.FullAuditedAggregateRoot();
        descriptor.MultiTenant();
    }
}
