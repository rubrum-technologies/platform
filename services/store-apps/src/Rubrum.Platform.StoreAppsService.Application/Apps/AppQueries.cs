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
    public static async Task<App?> GetAppByIdAsync(
        [ID<App>] Guid id,
        [Service] IMediator mediator,
        CancellationToken ct = default)
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
    public static async Task<IQueryable<App>> GetAppsAsync([Service] IMediator mediator, CancellationToken ct = default)
    {
        return await mediator.Send(new GetAppsQuery(), ct);
    }
}
