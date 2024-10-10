namespace Rubrum.Platform.DataSourceService;

public static class DataSourceServiceDbProperties
{
    public const string ConnectionStringName = "DataSourceService";

    public static string DbTablePrefix { get; set; } = "Rubrum";

    public static string? DbSchema { get; set; } = null;
}
