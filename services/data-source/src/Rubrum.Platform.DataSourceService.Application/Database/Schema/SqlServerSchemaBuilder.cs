namespace Rubrum.Platform.DataSourceService.Database.Schema;

public class SqlServerSchemaBuilder : IDatabaseSchemaBuilder
{
    public Task<DatabaseSchemaInformation> BuildAsync(string connectionString)
    {
        throw new NotImplementedException();
    }
}
