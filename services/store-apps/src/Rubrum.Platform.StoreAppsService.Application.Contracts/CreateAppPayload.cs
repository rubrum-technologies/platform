using Rubrum.Platform.StoreAppsService.Apps;

namespace Rubrum.Platform.StoreAppsService;

public sealed class CreateAppPayload(App app)
{
    public App App { get; } = app;
}
