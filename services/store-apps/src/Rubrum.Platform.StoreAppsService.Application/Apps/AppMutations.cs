using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Rubrum.Platform.StoreAppsService.Apps;

[MutationType]
public static class AppMutations
{
    public static async Task<App> CreateAppAsync(
        CreateAppInput input,
        [Service] IAppRepository appRepository,
        [Service] AppManager appManager,
        CancellationToken cancellationToken = default)
    {
        var app = await appManager.CreateAsync(
            input.TenantId,
            input.Name,
            input.Version,
            input.Enabled);

        await appRepository.InsertAsync(app, true, cancellationToken);

        return app;
    }

    [Error<AppNotFoundException>]
    public static async Task<App> ChangeNameAsync(
        UpdateAppInput input,
        [Service] IAppRepository appRepository,
        [Service] AppManager appManager,
        CancellationToken cancellationToken = default)
    {
        var app = await appRepository.FindAsync(x => x.Id == input.AppId, false, cancellationToken);

        if (app is null)
        {
           throw new AppNotFoundException();
        }

        await appManager.ChangeNameAsync(app, input.Name);

        return app;
    }

    [Error<AppNotFoundException>]
    public static async Task<App> DeleteAsync(
        [ID<App>] Guid appId,
        [Service] IAppRepository appRepository,
        CancellationToken cancellationToken = default)
    {
        var app = await appRepository.FindAsync(x => x.Id == appId, false, cancellationToken);

        if (app is null)
        {
            throw new AppNotFoundException();
        }

        await appRepository.DeleteAsync(app, true, cancellationToken);

        return app;
    }

    [Error<AppNotFoundException>]
    public static async Task<App> ActivateAsync(
        [ID<App>] Guid appId,
        [Service] IAppRepository appRepository,
        CancellationToken cancellationToken = default)
    {
        var app = await appRepository.FindAsync(x => x.Id == appId, false, cancellationToken);

        if (app is null)
        {
            throw new AppNotFoundException();
        }

        app.Activate();

        return app;
    }

    [Error<AppNotFoundException>]
    public static async Task<App> DeactivateAsync(
        [ID<App>] Guid appId,
        [Service] IAppRepository appRepository,
        CancellationToken cancellationToken = default)
    {
        var app = await appRepository.FindAsync(x => x.Id == appId, false, cancellationToken);

        if (app is null)
        {
            throw new AppNotFoundException();
        }

        app.Deactivate();

        return app;
    }
}
