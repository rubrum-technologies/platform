namespace Rubrum.Platform.DataSourceService.Database.Schema;

public interface IDatabaseSchemaBuilder
{
    Task<DatabaseSchemaInformation> BuildAsync(string connectionString);
}
