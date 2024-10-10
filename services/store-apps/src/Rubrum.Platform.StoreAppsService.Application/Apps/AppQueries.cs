using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.StoreAppsService.Apps.Queries;

namespace Rubrum.Platform.StoreAppsService.Apps;

[QueryType]
public static class AppQueries
{
    [Authorize]
    [NodeResolver]
    public static async Task<App?> GetAppByIdAsync(
        [ID<App>] Guid id,
        [Service] IMediator mediator,
        [Service] IAppByIdDataLoader blobByIdDataLoader,
        CancellationToken ct)
    {
        return await mediator.Send(
            new GetAppByIdQuery
            {
                Id = id,
            },
            ct);
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
