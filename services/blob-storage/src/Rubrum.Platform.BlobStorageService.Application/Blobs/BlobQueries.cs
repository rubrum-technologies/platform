using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Platform.BlobStorageService.Blobs.Queries;

namespace Rubrum.Platform.BlobStorageService.Blobs;

[QueryType]
public static class BlobQueries
{
    [Authorize]
    [NodeResolver]
    public static async Task<Blob?> GetBlobByIdAsync(
        [ID<Blob>] Guid id,
        [Service] IMediator mediator,
        CancellationToken ct)
    {
        return await mediator.Send(
            new GetBlobByIdQuery
            {
                Id = id,
            },
            ct);
    }
}
