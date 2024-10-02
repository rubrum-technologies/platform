using CookieCrumble;
using MySql.Data.MySqlClient;
using Rubrum.Platform.DataSourceService.Database.Exceptions;
using Rubrum.Platform.DataSourceService.Database.Schema;
using Shouldly;
using Testcontainers.MySql;
using Xunit;
using static Rubrum.Platform.DataSourceService.MySqlHelper;

namespace Rubrum.Platform.DataSourceService;

public sealed class MySqlSchemaBuilderTests : DataSourceServiceApplicationTestBase, IAsyncLifetime
{
    private readonly MySqlContainer _container = new MySqlBuilder().Build();
    private readonly MySqlSchemaBuilder _builder;

    public MySqlSchemaBuilderTests()
    {
        _builder = GetRequiredService<MySqlSchemaBuilder>();
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
        await using var connection = new MySqlConnection(_container.GetConnectionString());

        await connection.OpenAsync();

        await CreateTableUsersAsync(connection);

        var schema = await _builder.BuildAsync(_container.GetConnectionString());

        schema
            .ShouldNotBeNull()
            .MatchMarkdownSnapshot();
    }

    [Fact]
    public async Task Build_Users_or_Documents()
    {
        await using var connection = new MySqlConnection(_container.GetConnectionString());

        await connection.OpenAsync();

        await CreateTableUsersAsync(connection);
        await CreateTableDocumentsAsync(connection);

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
            await _builder.BuildAsync("Server=127.0.0.1;Port=63838;Database=test;Uid=mysql;Pwd=mysql");
        });
    }
}
