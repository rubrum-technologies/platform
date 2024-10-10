using Npgsql;

namespace Rubrum.Platform.DataSourceService;

public static class PostgresHelper
{
    public static async Task CreateTableUsersAsync(NpgsqlDataSource dataSource)
    {
        await using var command = dataSource
            .CreateCommand("CREATE TABLE users (id INT PRIMARY KEY, name VARCHAR(100) NOT NULL);");

        await command.ExecuteNonQueryAsync();
    }

    public static async Task CreateTableDocumentsAsync(NpgsqlDataSource dataSource)
    {
        await using var command = dataSource
            .CreateCommand(
                "CREATE TABLE documents (id INT PRIMARY KEY, name VARCHAR(100) NOT NULL, createAt date NOT NULL, userId bigint NOT NULL);");

        await command.ExecuteNonQueryAsync();
    }
}
