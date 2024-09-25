using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Rubrum.Platform.BlobStorageService.Folders.Commands;

[GraphQLName("ChangeFolderBlobParentIdInput")]
public class ChangeFolderBlobParentIdCommand : IRequest<FolderBlob>
{
    [ID<FolderBlob>]
    public required Guid Id { get; init; }

    [ID<FolderBlob>]
    public required Guid? ParentId { get; init; }

    public class Handler(
        FolderBlobManager manager,
        IFolderBlobRepository repository) : IRequestHandler<ChangeFolderBlobParentIdCommand, FolderBlob>
    {
        public async Task<FolderBlob> Handle(
            ChangeFolderBlobParentIdCommand request,
            CancellationToken cancellationToken)
        {
            var folder = await repository.GetAsync(request.Id, true, cancellationToken);

            await manager.ChangeParentIdAsync(folder, request.ParentId, cancellationToken);

            folder = await repository.UpdateAsync(folder, true, cancellationToken);

            return folder;
        }
    }
}
