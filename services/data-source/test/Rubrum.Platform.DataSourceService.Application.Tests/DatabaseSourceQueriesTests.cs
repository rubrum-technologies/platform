using CookieCrumble;
using MySql.Data.MySqlClient;
using Npgsql;
using Shouldly;
using Testcontainers.MySql;
using Testcontainers.PostgreSql;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public class DatabaseSourceQueriesTests : DataSourceServiceApplicationGraphqlTestBase
{
    [Fact]
    public async Task SchemaDatabase_MySql()
    {
        var container = new MySqlBuilder().Build();

        await container.StartAsync();

        await using var connection = new MySqlConnection(container.GetConnectionString());

        await connection.OpenAsync();

        await MySqlHelper.CreateTableUsersAsync(connection);
        await MySqlHelper.CreateTableDocumentsAsync(connection);

        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            $$"""
              query {
                  schemaDatabase(kind: MY_SQL, connectionString: "{{container.GetConnectionString()}}") {
                      ... on DatabaseSchemaInformation {
                          tables {
                              name
                              columns {
                                  kind
                                  name
                              }
                          }
                      }
                  }
              }
              """));

        await container.DisposeAsync().AsTask();

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SchemaDatabase_MySql_FailConnectException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                schemaDatabase(kind: MY_SQL, connectionString: "Server=127.0.0.1;Port=63838;Database=test;Uid=mysql;Pwd=mysql") {
                    ... on DatabaseSchemaInformation {
                        tables {
                            name
                            columns {
                                kind
                                name
                            }
                        }
                    }
                    ... on FailConnectError {
                      connectionString
                      message
                    }
                    __typename
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SchemaDatabase_MySql_IncorrectConnectionStringException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                schemaDatabase(kind: MY_SQL, connectionString: "some connection string mysql") {
                    ... on DatabaseSchemaInformation {
                        tables {
                            name
                            columns {
                                kind
                                name
                            }
                        }
                    }
                    ... on IncorrectConnectionStringError {
                      incorrectConnectionString
                      message
                    }
                    __typename
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SchemaDatabase_Postgresql()
    {
        var container = new PostgreSqlBuilder().Build();

        await container.StartAsync();

        await using var dataSource = NpgsqlDataSource.Create(container.GetConnectionString());

        await PostgresHelper.CreateTableUsersAsync(dataSource);
        await PostgresHelper.CreateTableDocumentsAsync(dataSource);

        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            $$"""
              query {
                  schemaDatabase(kind: POSTGRE_SQL, connectionString: "{{container.GetConnectionString()}}") {
                      ... on DatabaseSchemaInformation {
                          tables {
                              name
                              columns {
                                  kind
                                  name
                              }
                          }
                      }
                  }
              }
              """));

        await container.DisposeAsync().AsTask();

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SchemaDatabase_Postgresql_FailConnectException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                schemaDatabase(kind: POSTGRE_SQL, connectionString: "Host=127.0.0.1;Port=55200;Database=postgres;Username=postgres;Password=postgres") {
                    ... on DatabaseSchemaInformation {
                        tables {
                            name
                            columns {
                                kind
                                name
                            }
                        }
                    }
                    ... on FailConnectError {
                      connectionString
                      message
                    }
                    __typename
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SchemaDatabase_Postgresql_IncorrectConnectionStringException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                schemaDatabase(kind: POSTGRE_SQL, connectionString: "some connection string") {
                    ... on DatabaseSchemaInformation {
                        tables {
                            name
                            columns {
                                kind
                                name
                            }
                        }
                    }
                    ... on IncorrectConnectionStringError {
                      incorrectConnectionString
                      message
                    }
                    __typename
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_All()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceAll(order: {naming: ASC}) {
                    naming
                    tables(order: {naming: ASC}) {
                        systemName
                        naming
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_All_Filter()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceAll(where: {naming: {eq: "Test_Duplicate"}}, order: {naming: ASC}) {
                    naming
                    tables(order: {naming: ASC}) {
                        systemName
                        naming
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_All_FilterNested()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceAll(where: { tables: { some: { naming: {eq: "Source"} } } }, order: {naming: ASC}) {
                    naming
                    tables(order: {naming: ASC}) {
                        systemName
                        naming
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_First()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSource(order: {naming: ASC}) {
                    naming
                    tables(order: {naming: ASC}) {
                        systemName
                        naming
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_First_Filter()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSource(where: {naming: {eq: "West"}}, order: {naming: ASC}) {
                    naming
                    tables(order: {naming: ASC}) {
                        systemName
                        naming
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_First_FilterNested()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSource(where: { tables: { some: { systemName: {eq: "RubrumDatabaseTables"} } } }, order: {naming: ASC}) {
                    naming
                    tables(order: {naming: ASC}) {
                        systemName
                        naming
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_Any()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceAny
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_Any_Filter()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceAny(where: {naming: {eq: "West2"}})
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_Any_FilterNested()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceAny(where: { tables: { some: { systemName: {eq: "RubrumDataSources"} } } })
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_Count()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceCount
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_Count_Filter()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceCount(where: {naming: {eq: "West10"}})
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task SourceSelf_Count_FilterNested()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            query {
                westSourceCount(where: { tables: { some: { systemName: {eq: "RubrumDataSources"} } } })
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }
}
