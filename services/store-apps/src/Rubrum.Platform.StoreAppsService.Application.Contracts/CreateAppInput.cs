using HotChocolate.Types.Relay;
using Rubrum.Platform.StoreAppsService.Apps;
using Version = Rubrum.Platform.StoreAppsService.Apps.Version;

namespace Rubrum.Platform.StoreAppsService;

public sealed record CreateAppInput(
    [property: ID<App>] Guid? TenantId,
    string Name,
    Version Version,
    bool Enabled);
