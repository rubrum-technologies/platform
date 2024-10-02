using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;
using Rubrum.Graphql.Errors;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.BlobStorageService.Folders.Commands;

namespace Rubrum.Platform.BlobStorageService.Folders;

[MutationType]
public static class FolderBlobMutations
{
    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<FolderBlobNameAlreadyExistsException>]
    [Error<FolderBlobParentHasDifferentOwnerException>]
    public static async Task<FolderBlob> CreateFolderBlobAsync(
        CreateFolderBlobCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<FolderBlobNameAlreadyExistsException>]
    [Error<FolderBlobParentHasDifferentOwnerException>]
    public static async Task<FolderBlob> ChangeFolderBlobParentIdAsync(
        ChangeFolderBlobParentIdCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<FolderBlobNameAlreadyExistsException>]
    public static async Task<FolderBlob> ChangeFolderBlobNameAsync(
        ChangeFolderBlobNameCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }
}
