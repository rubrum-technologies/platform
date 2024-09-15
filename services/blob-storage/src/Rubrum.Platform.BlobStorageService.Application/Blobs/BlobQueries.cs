using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Rubrum.Authorization.Permissions;
using Rubrum.Graphql.Middlewares;

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
