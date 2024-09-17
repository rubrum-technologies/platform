using HotChocolate.Types.Relay;
using Rubrum.Platform.StoreAppsService.Apps;

namespace Rubrum.Platform.StoreAppsService;

public class ChangeNameAppInput
{
    [property: ID<App>]
    public Guid Id { get; init; }

    public required string Name { get; init; }
}
