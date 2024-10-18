using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;
using Rubrum.Graphql.Errors;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.WindowsService.Windows.Commands;

namespace Rubrum.Platform.WindowsService.Windows;

[MutationType]
public static class WindowMutations
{
    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<WindowNameAlreadyExistsException>]
    public static async Task<Window> CreateWindowAsync(
        CreateWindowCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<WindowNameAlreadyExistsException>]
    public static async Task<Window> ChangeWindowNameAsync(
        ChangeWindowNameCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    public static async Task<Window> ChangeWindowPositionAsync(
        ChangeWindowPositionCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    public static async Task<Window> ChangeWindowSizeAsync(
        ChangeWindowSizeCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }

    [Authorize]
    [UseUnitOfWork]
    [UseAbpError]
    public static async Task<Window> DeleteWindowAsync(
        DeleteWindowCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return await mediator.Send(input, ct);
    }
}
