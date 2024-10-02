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
                  schemaDatabase(kind: POSTGRESQL, connectionString: "{{container.GetConnectionString()}}") {
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
                  schemaDatabase(kind: POSTGRESQL, connectionString: "Host=127.0.0.1;Port=55200;Database=postgres;Username=postgres;Password=postgres") {
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
                schemaDatabase(kind: POSTGRESQL, connectionString: "some connection string") {
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
}
