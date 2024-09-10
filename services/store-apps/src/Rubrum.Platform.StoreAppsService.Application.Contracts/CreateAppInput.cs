namespace Rubrum.Platform.StoreAppsService;

public class CreateAppInput : CreateOrUpdateAppInputBase
{
    public required string Name { get; init; }
}
