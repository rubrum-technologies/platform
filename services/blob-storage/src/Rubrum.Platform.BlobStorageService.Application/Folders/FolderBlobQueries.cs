﻿using HotChocolate;
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
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(
            new GetFolderBlobByIdQuery
            {
                Id = id,
            },
            ct);
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
            new GetFoldersBlobQuery
            {
                ParentId = parentId,
            },
            ct);
    }
}
