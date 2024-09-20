using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Rubrum.Graphql.Errors;
using Rubrum.Platform.BlobStorageService.Folders.Commands;
using Volo.Abp.Uow;

namespace Rubrum.Platform.BlobStorageService.Folders;

[MutationType]
public static class FolderBlobMutations
{
    [UnitOfWork]
    [UseAbpError]
    [Error<FolderBlobNameAlreadyExistsException>]
    [Error<FolderBlobParentHasDifferentOwnerException>]
    public static Task<FolderBlob> CreateFolderBlobAsync(
        CreateFolderBlobCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return mediator.Send(input, ct);
    }
}
