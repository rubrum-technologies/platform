using MySql.Data.MySqlClient;

namespace Rubrum.Platform.DataSourceService;

public static class MySqlHelper
{
    public static async Task CreateTableUsersAsync(MySqlConnection connection)
    {
        await using var command = new MySqlCommand(
            "CREATE TABLE users (id INT PRIMARY KEY, name VARCHAR(100) NOT NULL);",
            connection);

        await command.ExecuteNonQueryAsync();
    }

    public static async Task CreateTableDocumentsAsync(MySqlConnection connection)
    {
        await using var command = new MySqlCommand(
            "CREATE TABLE documents (id INT PRIMARY KEY, name VARCHAR(100) NOT NULL, createAt DATE NOT NULL, userId BIGINT NOT NULL);",
            connection);

        await command.ExecuteNonQueryAsync();
    }
}
