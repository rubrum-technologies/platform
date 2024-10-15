using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;
using Rubrum.Graphql.Errors;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.StoreAppsService.Apps.Commands;

namespace Rubrum.Platform.StoreAppsService.Apps;

[MutationType]
public static class AppMutations
{
    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<AppNameAlreadyExistsException>]
    public static async Task<App> CreateAppAsync(
        CreateAppCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<AppNameAlreadyExistsException>]
    public static async Task<App> ChangeAppNameAsync(
        ChangeAppNameCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    public static async Task<App> DeleteAppAsync(
        DeleteAppCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    public static async Task<App> ActivateAppAsync(
        ActivateAppCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    public static async Task<App> DeactivateAppAsync(
        DeactivateAppCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }
}
