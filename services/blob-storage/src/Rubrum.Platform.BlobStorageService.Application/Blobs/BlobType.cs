using HotChocolate;
using HotChocolate.Types;
using Rubrum.Graphql.Ddd;

namespace Rubrum.Platform.BlobStorageService.Blobs;

[ObjectType<Blob>]
public static partial class BlobType
{
    public static string GetUrl([Parent] Blob blob)
    {
        return $"/api/blob-storage/blobs/{blob.Id}";
    }

    static partial void Configure(IObjectTypeDescriptor<Blob> descriptor)
    {
        descriptor.FullAuditedAggregateRoot();
        descriptor.MultiTenant();

        descriptor
            .ImplementsNode()
            .IdField(x => x.Id)
            .ResolveNode((context, id) => context.Service<IBlobByIdDataLoader>().LoadAsync(id, context.RequestAborted));

        descriptor.Ignore(x => x.Extension);
        descriptor.Ignore(x => x.SystemFileName);
        descriptor.Ignore(x => x.IsDisposable);
    }
}
