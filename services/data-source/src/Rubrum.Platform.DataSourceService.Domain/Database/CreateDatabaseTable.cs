namespace Rubrum.Platform.DataSourceService.Database;

public sealed record CreateDatabaseTable(string Name, string SystemName, IEnumerable<CreateDatabaseColumn> Columns);
