using CookieCrumble;
using Npgsql;
using Shouldly;
using Testcontainers.PostgreSql;
using Xunit;
using static Rubrum.Platform.DataSourceService.PostgresHelper;

namespace Rubrum.Platform.DataSourceService;

public class DatabaseSourceQueriesTests : DataSourceServiceApplicationGraphqlTestBase
{
    [Fact]
    public async Task SchemaDatabase_Postgresql()
    {
        var container = new PostgreSqlBuilder().Build();

        await container.StartAsync();

        await using var dataSource = NpgsqlDataSource.Create(container.GetConnectionString());

        await CreateTableUsersAsync(dataSource);
        await CreateTableDocumentsAsync(dataSource);

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
    public async Task SchemaDatabase_Postgresql_IncorrectСonnectionStringException()
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
