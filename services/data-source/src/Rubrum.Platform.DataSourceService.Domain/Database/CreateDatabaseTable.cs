namespace Rubrum.Platform.DataSourceService.Database;

public sealed record CreateDatabaseTable(
    Guid Id,
    string Name,
    string SystemName,
    IEnumerable<CreateDatabaseColumn> Columns);
