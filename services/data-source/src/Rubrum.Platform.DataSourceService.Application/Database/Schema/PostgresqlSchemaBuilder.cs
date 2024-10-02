using Npgsql;

namespace Rubrum.Platform.DataSourceService.Database.Schema;

public class PostgresqlSchemaBuilder : IDatabaseSchemaBuilder
{
    public async Task<DatabaseSchemaInformation> BuildAsync(string connectionString)
    {
        await using var dataSource = NpgsqlDataSource.Create(connectionString);

        return new DatabaseSchemaInformation
        {
            Tables = await GetTablesAsync(dataSource),
        };
    }

    private static async Task<IReadOnlyList<InfoAboutTable>> GetTablesAsync(NpgsqlDataSource dataSource)
    {
        var result = new List<InfoAboutTable>();

        await using var command = dataSource
            .CreateCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';");

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var name = reader["table_name"].ToString()!;

            result.Add(new InfoAboutTable
            {
                Name = name,
                Columns = await GetTableColumnsAsync(name, dataSource),
            });
        }

        return result;
    }

    private static async Task<IReadOnlyList<InfoAboutColumn>> GetTableColumnsAsync(
        string table,
        NpgsqlDataSource dataSource)
    {
        var result = new List<InfoAboutColumn>();
        await using var command = dataSource.CreateCommand(
            $"SELECT column_name, data_type FROM information_schema.columns WHERE table_name = '{table}'");
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var name = reader["column_name"].ToString()!;
            var type = reader["data_type"].ToString()!;

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
