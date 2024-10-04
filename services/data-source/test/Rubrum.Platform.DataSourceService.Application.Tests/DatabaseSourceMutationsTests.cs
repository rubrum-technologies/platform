using CookieCrumble;
using Shouldly;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public class DatabaseSourceMutationsTests : DataSourceServiceApplicationGraphqlTestBase
{
    [Fact]
    public async Task Create_MySql()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: MY_SQL,
                    name: "MySql",
                    prefix: "PR",
                    connectionString: "ConnectionStringTest",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_MySql_Test_1",
                            columns: [
                                {
                                    kind: UNKNOWN,
                                    name: "Column1",
                                    systemName: "Column_MySql_1",
                                },
                                {
                                    kind: STRING,
                                    name: "MySqlColumn2",
                                    systemName: "Column_2",
                                },
                                {
                                    kind: FLOAT,
                                    name: "Column3",
                                    systemName: "Column_3",
                                }
                            ]
                        },
                        {
                            name: "Table2",
                            systemName: "Table_2",
                            columns: [
                                {
                                    kind: DATE_TIME,
                                    name: "Column1",
                                    systemName: "Column_1",
                                },
                                {
                                    kind: BOOLEAN,
                                    name: "Column2",
                                    systemName: "Column_2",
                                },
                                {
                                    kind: STRING,
                                    name: "Column3",
                                    systemName: "Column_3",
                                }
                            ]
                        },
                    ]
                }) {
                    databaseSource {
                        kind
                        name
                        prefix
                        connectionString
                        tables {
                            name
                            systemName
                            columns {
                                kind
                                name
                                systemName
                                __typename
                            }
                            __typename
                        }
                        __typename
                    }
                    errors {
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_Postgres()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: POSTGRESQL,
                    name: "Postgres",
                    prefix: "PS",
                    connectionString: "ConnectionStringTest",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_1",
                            columns: [
                                {
                                    kind: BOOLEAN,
                                    name: "Column1",
                                    systemName: "Column_1",
                                },
                                {
                                    kind: STRING,
                                    name: "Column2",
                                    systemName: "Column_2",
                                },
                                {
                                    kind: INT,
                                    name: "Column3",
                                    systemName: "Column_3",
                                }
                            ]
                        },
                        {
                            name: "Table2",
                            systemName: "Table_2",
                            columns: [
                                {
                                    kind: DATE_TIME,
                                    name: "Column1",
                                    systemName: "Column_1",
                                },
                                {
                                    kind: UUID,
                                    name: "Column2",
                                    systemName: "Column_2",
                                },
                                {
                                    kind: FLOAT,
                                    name: "Column3",
                                    systemName: "Column_3",
                                }
                            ]
                        },
                    ]
                }) {
                    databaseSource {
                        kind
                        name
                        prefix
                        connectionString
                        tables {
                            name
                            systemName
                            columns {
                                kind
                                name
                                systemName
                                __typename
                            }
                            __typename
                        }
                        __typename
                    }
                    errors {
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DataSourceNameAlreadyExistsException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: POSTGRESQL,
                    name: "Test_Duplicate",
                    prefix: "ER",
                    connectionString: "ConnectionStringTest",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_1",
                            columns: [
                                {
                                    kind: BOOLEAN,
                                    name: "Column1",
                                    systemName: "Column_1",
                                }
                            ]
                        },
                    ]
                }) {
                    databaseSource {
                        name
                    }
                    errors {
                        ... on DataSourceNameAlreadyExistsError {
                            name
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DataSourcePrefixAlreadyExistsException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: POSTGRESQL,
                    name: "Test_Zaqq",
                    prefix: "Pr",
                    connectionString: "ConnectionStringTest",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_1",
                            columns: [
                                {
                                    kind: BOOLEAN,
                                    name: "Column1",
                                    systemName: "Column_1",
                                }
                            ]
                        },
                    ]
                }) {
                    databaseSource {
                        name
                        prefix
                    }
                    errors {
                        ... on DataSourcePrefixAlreadyExistsError {
                            prefix
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DatabaseSourceTablesEmptyException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: SQL_SERVER,
                    name: "Test98",
                    prefix: "Te",
                    connectionString: "Connection47",
                    tables: []
                }) {
                    databaseSource {
                        name
                        __typename
                    }
                    errors {
                        ... on DatabaseSourceTablesEmptyError {
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DatabaseTableNameAlreadyExistsException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: POSTGRESQL,
                    name: "Test10",
                    prefix: "Te",
                    connectionString: "ConnectionStringTest",
                    tables: [
                        {
                            name: "Table17",
                            systemName: "Table_1",
                            columns: [
                                {
                                    kind: BOOLEAN,
                                    name: "Column1",
                                    systemName: "Column_1",
                                }
                            ]
                        },
                        {
                            name: "Table17",
                            systemName: "Table_2",
                            columns: [
                                {
                                    kind: STRING,
                                    name: "Column1",
                                    systemName: "Column_1",
                                }
                            ]
                        },
                    ]
                }) {
                    databaseSource {
                        name
                        __typename
                    }
                    errors {
                        ... on DatabaseTableNameAlreadyExistsError {
                            tableName
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DatabaseTableSystemNameAlreadyExistsException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: SQL_SERVER,
                    name: "Test20",
                    prefix: "Err",
                    connectionString: "ConnectionStringTest1",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_19",
                            columns: [
                                {
                                    kind: BOOLEAN,
                                    name: "Column1",
                                    systemName: "Column_1",
                                }
                            ]
                        },
                        {
                            name: "Table2",
                            systemName: "Table_19",
                            columns: [
                                {
                                    kind: STRING,
                                    name: "Column1",
                                    systemName: "Column_1",
                                }
                            ]
                        },
                    ]
                }) {
                    databaseSource {
                        name
                        __typename
                    }
                    errors {
                        ... on DatabaseTableSystemNameAlreadyExistsError {
                            tableSystemName
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DatabaseTableColumnsEmptyException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: MY_SQL,
                    name: "Test56",
                    prefix: "Err",
                    connectionString: "ConnectionStringTest47",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_1",
                            columns: []
                        }
                    ]
                }) {
                    databaseSource {
                        name
                        __typename
                    }
                    errors {
                        ... on DatabaseTableColumnsEmptyError {
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DatabaseColumnNameAlreadyExistsException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: SQL_SERVER,
                    name: "Test20",
                    prefix: "Err",
                    connectionString: "ConnectionStringTest1",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_1",
                            columns: [
                                {
                                    kind: BOOLEAN,
                                    name: "Column10",
                                    systemName: "Column_1",
                                },
                                {
                                    kind: STRING,
                                    name: "Column10",
                                    systemName: "Column_2",
                                }
                            ]
                        }
                    ]
                }) {
                    databaseSource {
                        name
                        __typename
                    }
                    errors {
                        ... on DatabaseColumnNameAlreadyExistsError {
                            columnName
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task Create_DatabaseColumnSystemNameAlreadyExistsException()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            """
            mutation {
                createDatabaseSource(input: {
                    kind: MY_SQL,
                    name: "Test50",
                    prefix: "Err",
                    connectionString: "ConnectionStringTest15",
                    tables: [
                        {
                            name: "Table1",
                            systemName: "Table_1",
                            columns: [
                                {
                                    kind: BOOLEAN,
                                    name: "Column1",
                                    systemName: "Column_5",
                                },
                                {
                                    kind: UNKNOWN,
                                    name: "Column2",
                                    systemName: "Column_5",
                                }
                            ]
                        }
                    ]
                }) {
                    databaseSource {
                        name
                        __typename
                    }
                    errors {
                        ... on DatabaseColumnSystemNameAlreadyExistsError {
                            columnSystemName
                            code
                            details
                            logLevel
                            message
                        }
                        __typename
                    }
                }
            }
            """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }
}
