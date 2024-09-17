using Version = Rubrum.Platform.StoreAppsService.Apps.Version;

namespace Rubrum.Platform.StoreAppsService;

public class CreateAppInput
{
    public string Name { get; init; }

    public Version Version { get; init; }

    public Guid? TenantId { get; init; }

    public bool Enabled { get; init; }
}
