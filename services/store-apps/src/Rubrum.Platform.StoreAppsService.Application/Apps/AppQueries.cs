using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Rubrum.Graphql.Middlewares;

namespace Rubrum.Platform.StoreAppsService.Apps;

[QueryType]
public static class AppQueries
{
    [Authorize]
    [NodeResolver]
    public static Task<App?> GetAppByIdAsync(
        [ID<App>] Guid id,
        [Service] IAppByIdDataLoader blobByIdDataLoader,
        CancellationToken cancellationToken)
    {
        return blobByIdDataLoader.LoadAsync(id, cancellationToken);
    }

    [Authorize]
    [UseUnitOfWork]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static Task<IQueryable<App>> GetAppsAsync([Service] IAppRepository repository)
    {
        return repository.GetQueryableAsync();
    }
}
