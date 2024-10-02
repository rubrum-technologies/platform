namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSourceManager : DataSourceManager
{
    public async Task<DatabaseSource> CreateAsync(
        DatabaseKind kind,
        string name,
        string connectionString,
        IEnumerable<CreateDatabaseTable> tables,
        string? prefix = null)
    {
        await CheckNameAsync(name);

        return new DatabaseSource(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            kind,
            name,
            connectionString,
            tables,
            prefix);
    }
}
