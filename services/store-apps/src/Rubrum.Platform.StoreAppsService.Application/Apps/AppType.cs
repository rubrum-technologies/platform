using HotChocolate.Types;
using Rubrum.Graphql;

namespace Rubrum.Platform.StoreAppsService.Apps;

[ObjectType<App>]
public static partial class AppType
{
    static partial void Configure(IObjectTypeDescriptor<App> descriptor)
    {
        descriptor.FullAuditedAggregateRoot();
        descriptor.MultiTenant();

        descriptor
            .ImplementsNode()
            .IdField(x => x.Id)
            .ResolveNode((context, id) => context.Service<IAppByIdDataLoader>().LoadAsync(id, context.RequestAborted));
    }
}
