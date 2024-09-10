namespace Rubrum.Platform.StoreAppsService;

public static class StoreAppsServiceDbProperties
{
    public const string ConnectionStringName = "StoreAppsService";

    public static string DbTablePrefix { get; set; } = "Rubrum.Platform";

    public static string? DbSchema { get; set; } = null;
}
