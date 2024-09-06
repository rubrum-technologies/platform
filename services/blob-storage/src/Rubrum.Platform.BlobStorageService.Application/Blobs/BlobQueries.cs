using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.BlobStorageService.Permissions;

namespace Rubrum.Platform.BlobStorageService.Blobs;

[QueryType]
public static class BlobQueries
{
    [Authorize(Policy = BlobStorageServicePermissions.Blobs.Default)]
    [NodeResolver]
    public static async Task<Blob?> GetBlobByIdAsync(
        [ID<Blob>] Guid id,
        [Service] IBlobByIdDataLoader blobByIdDataLoader,
        CancellationToken cancellationToken)
    {
        return await blobByIdDataLoader.LoadAsync(id, cancellationToken);
    }

    [Authorize(Policy = BlobStorageServicePermissions.Blobs.Default)]
    [UseUnitOfWork]
    [UseFirstOrDefault]
    [UseFiltering]
    public static Task<IQueryable<Blob>> GetBlobAsync([Service] IBlobRepository repository)
    {
        return repository.GetQueryableAsync();
    }

    [Authorize(Policy = BlobStorageServicePermissions.Blobs.Default)]
    [UseUnitOfWork]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static Task<IQueryable<Blob>> GetBlobsAsync([Service] IBlobRepository repository)
    {
        return repository.GetQueryableAsync();
    }

    [Authorize(Policy = BlobStorageServicePermissions.Blobs.Default)]
    [UseUnitOfWork]
    [UseAny]
    [UseFiltering]
    public static Task<IQueryable<Blob>> GetBlobsAnyAsync([Service] IBlobRepository repository)
    {
        return repository.GetQueryableAsync();
    }

    [Authorize(Policy = BlobStorageServicePermissions.Blobs.Default)]
    [UseUnitOfWork]
    [UseCount]
    [UseFiltering]
    public static Task<IQueryable<Blob>> GetBlobsCountAsync([Service] IBlobRepository repository)
    {
        return repository.GetQueryableAsync();
    }
}
