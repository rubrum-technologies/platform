using MediatR;
using Microsoft.AspNetCore.Authorization;
using Rubrum.Authorization.Relations;

namespace Rubrum.Platform.BlobStorageService.Folders.Queries;

public class GetFolderBlobByIdQuery : IRequest<FolderBlob?>
{
    public required Guid Id { get; init; }

    public class Handler(
        IFolderBlobByIdDataLoader dataLoader,
        IAuthorizationService authorization) : IRequestHandler<GetFolderBlobByIdQuery, FolderBlob?>
    {
        public async Task<FolderBlob?> Handle(GetFolderBlobByIdQuery request, CancellationToken cancellationToken)
        {
            await authorization.CheckAsync<FolderBlob>(FolderBlobDefinition.View, request.Id);

            return await dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}
