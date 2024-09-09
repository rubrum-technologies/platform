namespace Rubrum.Platform.BlobStorageService;

public static class BlobStorageServiceDbProperties
{
    public const string ConnectionStringName = "blob-storage-service-db";

    public static string DbTablePrefix { get; set; } = "Rubrum";

    public static string? DbSchema { get; set; } = null;
}
