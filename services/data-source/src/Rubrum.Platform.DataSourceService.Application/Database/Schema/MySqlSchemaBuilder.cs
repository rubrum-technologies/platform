namespace Rubrum.Platform.DataSourceService.Database.Schema;

public class MySqlSchemaBuilder : IDatabaseSchemaBuilder
{
    public Task<DatabaseSchemaInformation> BuildAsync(string connectionString)
    {
        throw new NotImplementedException();
    }
}
