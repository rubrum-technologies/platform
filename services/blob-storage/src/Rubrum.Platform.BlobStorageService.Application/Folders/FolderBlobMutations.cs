using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Rubrum.Graphql.Errors;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.BlobStorageService.Folders.Commands;

namespace Rubrum.Platform.BlobStorageService.Folders;

[MutationType]
public static class FolderBlobMutations
{
    [UseUnitOfWork]
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

    [UseUnitOfWork]
    [UseAbpError]
    [Error<FolderBlobNameAlreadyExistsException>]
    [Error<FolderBlobParentHasDifferentOwnerException>]
    public static Task<FolderBlob> ChangeFolderBlobParentIdAsync(
        ChangeFolderBlobParentIdCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return mediator.Send(input, ct);
    }

    [UseUnitOfWork]
    [UseAbpError]
    [Error<FolderBlobNameAlreadyExistsException>]
    public static Task<FolderBlob> ChangeFolderBlobNameAsync(
        ChangeFolderBlobNameCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return mediator.Send(input, ct);
    }
}
