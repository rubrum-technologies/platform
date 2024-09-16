using HotChocolate.Types.Relay;
using Rubrum.Platform.StoreAppsService.Apps;

namespace Rubrum.Platform.StoreAppsService;

public sealed record UpdateAppInput(
    [property: ID<App>] Guid AppId,
    string Name);
