using Npgsql;
using Rubrum.Platform.DataSourceService.Database.Exceptions;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database.Schema;

[ExposeServices(typeof(PostgresqlSchemaBuilder))]
public class PostgresqlSchemaBuilder : IDatabaseSchemaBuilder, ITransientDependency
{
    public async Task<DatabaseSchemaInformation> BuildAsync(string connectionString)
    {
        try
        {
            await using var dataSource = NpgsqlDataSource.Create(connectionString);

            return new DatabaseSchemaInformation
            {
                Tables = await GetTablesAsync(dataSource),
            };
        }
        catch (ArgumentException ex) when (ex.Message.StartsWith("Format of the initialization string"))
        {
            throw new IncorrectConnectionStringException(ex.Message, connectionString);
        }
        catch (NpgsqlException ex) when (ex.Message.StartsWith("Failed to connect"))
        {
            throw new FailConnectException(ex.Message, connectionString);
        }
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
                    "boolean" => DataSourceEntityPropertyKind.Boolean,
                    "integer" => DataSourceEntityPropertyKind.Int,
                    "character varying" => DataSourceEntityPropertyKind.String,
                    _ => DataSourceEntityPropertyKind.Unknown,
                },
                Name = name,
            });
        }

        return result;
    }
}
