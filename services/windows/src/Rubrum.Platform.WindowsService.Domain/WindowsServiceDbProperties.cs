namespace Rubrum.Platform.WindowsService;

public static class WindowsServiceDbProperties
{
    public const string ConnectionStringName = "windows-service-db";

    public static string DbTablePrefix { get; set; } = "Rubrum";

    public static string? DbSchema { get; set; } = null;
}
