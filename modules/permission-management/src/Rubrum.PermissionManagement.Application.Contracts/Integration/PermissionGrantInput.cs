namespace Rubrum.PermissionManagement.Integration;

public class PermissionGrantInput
{
    public required string[] Names { get; init; }

    public required string ProviderName { get; init; }

    public required string ProviderKey { get; init; }
}
