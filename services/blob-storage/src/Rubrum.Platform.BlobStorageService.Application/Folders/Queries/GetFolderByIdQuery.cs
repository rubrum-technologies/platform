using MediatR;
using Microsoft.AspNetCore.Authorization;
using Rubrum.Authorization.Relations;

namespace Rubrum.Platform.BlobStorageService.Folders.Queries;

public class GetFolderByIdQuery : IRequest<FolderBlob?>
{
    public required Guid Id { get; init; }

    public class Handler(
        IFolderBlobByIdDataLoader dataLoader,
        IAuthorizationService authorization) : IRequestHandler<GetFolderByIdQuery, FolderBlob?>
    {
        public async Task<FolderBlob?> Handle(GetFolderByIdQuery request, CancellationToken cancellationToken)
        {
            await authorization.CheckAsync<FolderBlob>(FolderBlobDefinition.View, request.Id);

            return await dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}
