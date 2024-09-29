using HotChocolate;
using HotChocolate.Types;
using Rubrum.Graphql;
using Rubrum.Graphql.Relations;
using Rubrum.Platform.BlobStorageService.Folders;

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
        descriptor.Owner();
        descriptor.BindDefinition(typeof(BlobDefinition));

        descriptor
            .ImplementsNode()
            .IdField(x => x.Id);

        descriptor
            .Field(x => x.FolderId)
            .ID<FolderBlob>();

        descriptor.Field(x => x.Metadata);
    }
}
