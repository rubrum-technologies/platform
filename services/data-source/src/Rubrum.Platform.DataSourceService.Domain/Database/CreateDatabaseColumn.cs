namespace Rubrum.Platform.DataSourceService.Database;

public sealed record CreateDatabaseColumn(DataSourceEntityPropertyKind Kind, string Name, string SystemName);
