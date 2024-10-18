using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.WindowsService.Windows.Queries;

namespace Rubrum.Platform.WindowsService.Windows;

[QueryType]
public static class WindowQueries
{
    [Authorize]
    public static async Task<Window?> GetWindowByIdAsync(
        [ID<Window>] Guid id,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(
            new GetWindowByIdQuery { Id = id, },
            ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Window>> GetWindowsAsync(
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(new GetWindowsQuery(), ct);
    }
}
