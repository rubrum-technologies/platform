namespace Rubrum.Platform.DataSourceService.Database.Schema;

public class InfoAboutColumn
{
    public required DatabaseColumnKind Kind { get; init; }

    public required string Name { get; init; }
}
