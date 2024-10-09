namespace Rubrum.Platform.DataSourceService.Database;

public sealed record CreateDatabaseColumn(Guid Id, DataSourceEntityPropertyKind Kind, string Name, string SystemName);
