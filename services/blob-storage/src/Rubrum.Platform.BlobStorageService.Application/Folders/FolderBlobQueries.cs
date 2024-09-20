using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Platform.BlobStorageService.Folders.Queries;

namespace Rubrum.Platform.BlobStorageService.Folders;

[QueryType]
public static class FolderBlobQueries
{
    [Authorize]
    [NodeResolver]
    public static async Task<FolderBlob?> GetFolderBlobByIdAsync(
        [ID<FolderBlob>] Guid id,
        [Service] IFolderBlobByIdDataLoader dataLoader,
        CancellationToken ct = default)
    {
        return await dataLoader.LoadAsync(id, ct);
    }

    [Authorize]
    [UseFiltering]
    [UseSorting]
    public static Task<IQueryable<FolderBlob>> GetFoldersAsync(
        [ID<FolderBlob>] Guid? parentId,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return mediator.Send(
            new GetFoldersQuery
            {
                ParentId = parentId,
            },
            ct);
    }
}
