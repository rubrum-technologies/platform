namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSourceManager : DataSourceManager
{
    public async Task<DatabaseSource> CreateAsync(
        DatabaseKind kind,
        string name,
        string prefix,
        string connectionString,
        IEnumerable<CreateDatabaseTable> tables)
    {
        await CheckNameAsync(name);
        await CheckPrefixAsync(prefix);

        return new DatabaseSource(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            kind,
            name,
            prefix,
            connectionString,
            tables);
    }
}
