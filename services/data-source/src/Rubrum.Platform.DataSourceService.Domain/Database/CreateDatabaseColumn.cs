namespace Rubrum.Platform.DataSourceService.Database;

public sealed record CreateDatabaseColumn(DatabaseColumnKind Kind, string Name, string SystemName);
