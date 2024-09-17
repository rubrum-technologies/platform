using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Rubrum.Platform.BlobStorageService.Blobs;

[QueryType]
public static class BlobQueries
{
    [Authorize]
    [NodeResolver]
    public static async Task<Blob?> GetBlobByIdAsync(
        [ID<Blob>] Guid id,
        [Service] IBlobByIdDataLoader blobByIdDataLoader,
        CancellationToken cancellationToken)
    {
        return await blobByIdDataLoader.LoadAsync(id, cancellationToken);
    }
}
