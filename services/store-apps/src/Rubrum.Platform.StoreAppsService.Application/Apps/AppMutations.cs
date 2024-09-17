using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Rubrum.Graphql.Middlewares;

namespace Rubrum.Platform.StoreAppsService.Apps;

[MutationType]
public static class AppMutations
{
    [UseUnitOfWork]
    public static async Task<App> CreateAppAsync(
        CreateAppInput input,
        [Service] IAppRepository repository,
        [Service] AppManager manager,
        CancellationToken ct = default)
    {
        var app = await manager.CreateAsync(
            input.TenantId,
            input.Name,
            input.Version,
            input.Enabled);

        await repository.InsertAsync(app, true, ct);

        return app;
    }

    [UseUnitOfWork]
    public static async Task<App> ChangeNameAppAsync(
        ChangeNameAppInput input,
        [Service] IAppRepository repository,
        [Service] AppManager manager,
        CancellationToken ct = default)
    {
        var app = await repository.GetAsync(x => x.Id == input.Id, true, ct);

        await manager.ChangeNameAsync(app, input.Name);

        return app;
    }

    [UseUnitOfWork]
    public static async Task<App> DeleteAppAsync(
        [ID<App>] Guid id,
        [Service] IAppRepository repository,
        CancellationToken ct = default)
    {
        var app = await repository.GetAsync(x => x.Id == id, true, ct);

        await repository.DeleteAsync(app, true, ct);

        return app;
    }

    [UseUnitOfWork]
    public static async Task<App> ActivateAppAsync(
        [ID<App>] Guid id,
        [Service] IAppRepository repository,
        CancellationToken ct = default)
    {
        var app = await repository.GetAsync(x => x.Id == id, true, ct);

        app.Activate();

        await repository.UpdateAsync(app, true, ct);

        return app;
    }

    [UseUnitOfWork]
    public static async Task<App> DeactivateAppAsync(
        [ID<App>] Guid id,
        [Service] IAppRepository repository,
        CancellationToken ct = default)
    {
        var app = await repository.GetAsync(x => x.Id == id, true, ct);

        app.Deactivate();

        await repository.UpdateAsync(app, true, ct);

        return app;
    }
}
