using MySql.Data.MySqlClient;
using Npgsql;
using Rubrum.Platform.DataSourceService.Database.Exceptions;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database.Schema;

[ExposeServices(typeof(MySqlSchemaBuilder))]
public class MySqlSchemaBuilder : IDatabaseSchemaBuilder, ITransientDependency
{
    public async Task<DatabaseSchemaInformation> BuildAsync(string connectionString)
    {
        try
        {
            await using var connection = new MySqlConnection(connectionString);

            await connection.OpenAsync();

            return new DatabaseSchemaInformation
            {
                Tables = await GetTablesAsync(connection),
            };
        }
        catch (ArgumentException ex) when (ex.Message.StartsWith("Format of the initialization string"))
        {
            throw new IncorrectConnectionStringException(ex.Message, connectionString);
        }
        catch (MySqlException ex) when (ex.Message.StartsWith("Unable to connect to any of the specified MySQL hosts"))
        {
            throw new FailConnectException(ex.Message, connectionString);
        }
    }

    private static async Task<IReadOnlyList<InfoAboutTable>> GetTablesAsync(MySqlConnection connection)
    {
        var result = new List<InfoAboutTable>();
        var tables = new List<string>();

        await using var command = new MySqlCommand("SHOW TABLES", connection);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var name = reader[0].ToString()!;

            tables.Add(name);
        }

        await reader.CloseAsync();

        foreach (var table in tables)
        {
            result.Add(new InfoAboutTable
            {
                Name = table,
                Columns = await GetTableColumnsAsync(table, connection),
            });
        }

        return result;
    }

    private static async Task<IReadOnlyList<InfoAboutColumn>> GetTableColumnsAsync(
        string table,
        MySqlConnection connection)
    {
        var result = new List<InfoAboutColumn>();
        await using var command = new MySqlCommand(
            $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'",
            connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var name = reader["COLUMN_NAME"].ToString()!;
            var type = reader["DATA_TYPE"].ToString()!;

            result.Add(new InfoAboutColumn
            {
                // TODO: Доделать сопоставление типов.
                Kind = type switch
                {
                    "boolean" => DatabaseColumnKind.Boolean,
                    "integer" => DatabaseColumnKind.Number,
                    "character varying" => DatabaseColumnKind.String,
                    _ => DatabaseColumnKind.Unknown,
                },
                Name = name,
            });
        }

        return result;
    }
}
