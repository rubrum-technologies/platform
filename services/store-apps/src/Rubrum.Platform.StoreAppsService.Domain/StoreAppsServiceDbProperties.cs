namespace Rubrum.Platform.StoreAppsService;

public static class StoreAppsServiceDbProperties
{
    public const string ConnectionStringName = "store-apps-service-db";

    public static string DbTablePrefix { get; set; } = "Rubrum";

    public static string? DbSchema { get; set; } = null;
}
