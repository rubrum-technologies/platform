using Version = Rubrum.Platform.StoreAppsService.Apps.Version;

namespace Rubrum.Platform.StoreAppsService;

public class CreateAppInput
{
    public required string Name { get; init; }

    public required Version Version { get; init; }

    public bool Enabled { get; init; }
}
