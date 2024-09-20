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
        [Service] IBlobByIdDataLoader dataLoader,
        CancellationToken ct)
    {
        return await dataLoader.LoadAsync(id, ct);
    }
}
