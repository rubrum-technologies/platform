namespace Rubrum.Platform.BlobStorageService;

public static class BlobStorageServiceDbProperties
{
    public const string ConnectionStringName = "BlobStorageService";

    public static string DbTablePrefix { get; set; } = "Rubrum.Platform";

    public static string? DbSchema { get; set; } = null;
}
