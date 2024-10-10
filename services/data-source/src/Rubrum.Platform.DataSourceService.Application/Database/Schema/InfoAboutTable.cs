namespace Rubrum.Platform.DataSourceService.Database.Schema;

public class InfoAboutTable
{
    public required string Name { get; init; }

    public required IReadOnlyList<InfoAboutColumn> Columns { get; init; }
}
