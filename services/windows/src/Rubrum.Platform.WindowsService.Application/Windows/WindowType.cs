using HotChocolate.Types;
using Rubrum.Graphql;

namespace Rubrum.Platform.WindowsService.Windows;

[ObjectType<Window>]
public static partial class WindowType
{
    static partial void Configure(IObjectTypeDescriptor<Window> descriptor)
    {
        descriptor.FullAuditedAggregateRoot();
        descriptor.MultiTenant();
        descriptor.Owner();

        descriptor
            .ImplementsNode()
            .IdField(x => x.Id)
            .ResolveNode(
                (context, id) => context.Service<IWindowByIdDataLoader>().LoadAsync(id, context.RequestAborted));

        descriptor.Field(x => x.Name);
        descriptor.Field(x => x.AppId);
        descriptor.Field(x => x.Position);
        descriptor.Field(x => x.Size);
    }
}
