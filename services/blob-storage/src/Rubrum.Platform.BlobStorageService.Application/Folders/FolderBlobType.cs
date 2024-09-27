using GreenDonut;
using HotChocolate;
using HotChocolate.Types;
using Rubrum.Graphql;
using Rubrum.Graphql.Ddd;
using Rubrum.Graphql.Relations;
using Rubrum.Platform.BlobStorageService.Blobs;

namespace Rubrum.Platform.BlobStorageService.Folders;

[ObjectType<FolderBlob>]
public static partial class FolderBlobType
{
    public static async Task<FolderBlob?> GetParentAsync(
        [Parent] FolderBlob folder,
        [Service] IFolderBlobByIdDataLoader dataLoader,
        CancellationToken ct = default)
    {
        if (folder.ParentId is null)
        {
            return null;
        }

        return await dataLoader.LoadRequiredAsync(folder.ParentId.Value, ct);
    }

    public static Task<Blob[]> GetBlobsAsync(
        [Parent] FolderBlob folder,
        [Service] IBlobsByFolderIdDataLoader dataLoader,
        CancellationToken ct = default)
    {
        return dataLoader.LoadRequiredAsync(folder.Id, ct);
    }

    static partial void Configure(IObjectTypeDescriptor<FolderBlob> descriptor)
    {
        descriptor.FullAuditedAggregateRoot();
        descriptor.MultiTenant();
        descriptor.Owner();
        descriptor.BindDefinition(typeof(FolderBlobDefinition));

        descriptor
            .ImplementsNode()
            .IdField(x => x.Id);

        descriptor
            .Field(x => x.ParentId)
            .ID<FolderBlob>();
    }
}
