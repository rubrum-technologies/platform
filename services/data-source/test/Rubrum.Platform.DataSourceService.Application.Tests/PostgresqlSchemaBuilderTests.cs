using CookieCrumble;
using Npgsql;
using Rubrum.Platform.DataSourceService.Database.Exceptions;
using Rubrum.Platform.DataSourceService.Database.Schema;
using Shouldly;
using Testcontainers.PostgreSql;
using Xunit;
using static Rubrum.Platform.DataSourceService.PostgresHelper;

namespace Rubrum.Platform.DataSourceService;

public sealed class PostgresqlSchemaBuilderTests : DataSourceServiceApplicationTestBase, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder().Build();
    private readonly PostgresqlSchemaBuilder _builder;

    public PostgresqlSchemaBuilderTests()
    {
        _builder = GetRequiredService<PostgresqlSchemaBuilder>();
    }

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
        await using var dataSource = NpgsqlDataSource.Create(_container.GetConnectionString());

        await CreateTableUsersAsync(dataSource);

        var schema = await _builder.BuildAsync(_container.GetConnectionString());

        schema
            .ShouldNotBeNull()
            .MatchMarkdownSnapshot();
    }

    [Fact]
    public async Task Build_Users_or_Documents()
    {
        await using var dataSource = NpgsqlDataSource.Create(_container.GetConnectionString());

        await CreateTableUsersAsync(dataSource);
        await CreateTableDocumentsAsync(dataSource);

        var schema = await _builder.BuildAsync(_container.GetConnectionString());

        schema
            .ShouldNotBeNull()
            .MatchMarkdownSnapshot();
    }

    [Fact]
    public async Task Build_IncorrectConnectionStringException()
    {
        await Assert.ThrowsAsync<IncorrectConnectionStringException>(async () =>
        {
            await _builder.BuildAsync("TestConnection");
        });
    }

    [Fact]
    public async Task Build_FailConnectException()
    {
        await Assert.ThrowsAsync<FailConnectException>(async () =>
        {
            await _builder.BuildAsync(
                "Host=127.0.0.1;Port=55217;Database=postgres;Username=postgres;Password=postgres");
        });
    }
}
