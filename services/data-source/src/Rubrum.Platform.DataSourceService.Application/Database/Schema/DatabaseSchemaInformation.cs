namespace Rubrum.Platform.DataSourceService.Database.Schema;

public class DatabaseSchemaInformation
{
    public required IReadOnlyList<InfoAboutTable> Tables { get; init; }
}
