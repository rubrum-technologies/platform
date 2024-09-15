using Version = Rubrum.Platform.StoreAppsService.Apps.Version;

namespace Rubrum.Platform.StoreAppsService;

public class CreateOrUpdateAppInputBase
{
    public Version Version { get; init; }

    public bool Enabled { get; init; }
}
