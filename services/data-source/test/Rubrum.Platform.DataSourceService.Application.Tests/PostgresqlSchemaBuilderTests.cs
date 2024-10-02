using CookieCrumble;
using Npgsql;
using Rubrum.Platform.DataSourceService.Database.Schema;
using Shouldly;
using Testcontainers.PostgreSql;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public sealed class PostgresqlSchemaBuilderTests : DataSourceServiceApplicationTestBase, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync().AsTask();
    }

    [Fact]
    public async Task Build_Users()
    {
        var builder = new PostgresqlSchemaBuilder();
        await using var dataSource = NpgsqlDataSource.Create(_container.GetConnectionString());

        await CreateTableUsersAsync(dataSource);

        var schema = await builder.BuildAsync(_container.GetConnectionString());

        schema
            .ShouldNotBeNull()
            .MatchMarkdownSnapshot();
    }

    [Fact]
    public async Task Build_Users_or_Documents()
    {
        var builder = new PostgresqlSchemaBuilder();
        await using var dataSource = NpgsqlDataSource.Create(_container.GetConnectionString());

        await CreateTableUsersAsync(dataSource);
        await CreateTableDocumentsAsync(dataSource);

        var schema = await builder.BuildAsync(_container.GetConnectionString());

        schema
            .ShouldNotBeNull()
            .MatchMarkdownSnapshot();
    }

    private static async Task CreateTableUsersAsync(NpgsqlDataSource dataSource)
    {
        await using var command = dataSource
            .CreateCommand("CREATE TABLE users (id INT PRIMARY KEY, name VARCHAR(100) NOT NULL);");

        await command.ExecuteNonQueryAsync();
    }

    private static async Task CreateTableDocumentsAsync(NpgsqlDataSource dataSource)
    {
        await using var command = dataSource
            .CreateCommand(
                "CREATE TABLE documents (id INT PRIMARY KEY, name VARCHAR(100) NOT NULL, createAt date NOT NULL, userId bigint NOT NULL);");

        await command.ExecuteNonQueryAsync();
    }
}
