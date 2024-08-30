namespace Rubrum.Platform.AdministrationService;

public static class AdministrationServiceDbProperties
{
    public const string ConnectionStringName = "administration-service-db";

    public static string DbTablePrefix { get; set; } = "Rubrum";

    public static string? DbSchema { get; set; } = null;
}
