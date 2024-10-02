using HotChocolate.Types.Relay;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Rubrum.Authorization.Relations;

namespace Rubrum.Platform.BlobStorageService.Blobs.Queries;

public class GetBlobByIdQuery : IRequest<Blob?>
{
    [ID<Blob>]
    public required Guid Id { get; init; }

    public class Handler(
        IAuthorizationService authorization,
        IBlobByIdDataLoader dataLoader) : IRequestHandler<GetBlobByIdQuery, Blob?>
    {
        public async Task<Blob?> Handle(GetBlobByIdQuery request, CancellationToken cancellationToken)
        {
            await authorization.CheckAsync<Blob>(BlobDefinition.View, request.Id);

            return await dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}
