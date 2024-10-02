using Rubrum.Platform.DataSourceService.Database;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public class DatabaseSourceTests
{
    [Theory]
    [InlineData("Test_Name")]
    [InlineData("DataSource")]
    [InlineData("SomeName")]
    public void SetName(string name)
    {
        var source = CreateDatabaseSource();

        source.SetName(name);

        source.Name.ShouldBe(name);
    }

    [Fact]
    public void SetName_Empty()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<ArgumentException>(() => { source.SetName(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetName_MaxLenght()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<ArgumentException>(() =>
        {
            source.SetName(string.Empty.PadRight(DataSourceConstants.NameLength + 1, '-'));
        });
    }

    [Fact]
    public void GetTableById()
    {
        var source = CreateDatabaseSource();

        var tableId = source.Tables[0].Id;

        var table = source.GetTableById(tableId);

        table.ShouldNotBeNull();
        table.Id.ShouldBe(tableId);
    }

    [Fact]
    public void GetTableId_EntityNotFoundException()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<EntityNotFoundException>(() => { source.GetTableById(Guid.NewGuid()); });
    }

    [Theory]
    [InlineData("Table1", "_Table1_")]
    [InlineData("TableCustom", "Table__Custom__2")]
    [InlineData("TableSql", "Table__Sql_10")]
    public void AddTable(string name, string systemName)
    {
        var source = CreateDatabaseSource();

        var table = source.AddTable(
            name,
            systemName,
            [new CreateDatabaseColumn(DatabaseColumnKind.Unknown, "Column", "ColumnS")]);

        table.DatabaseSourceId.ShouldBe(table.Id);
        table.Name.ShouldBe(name);
        table.SystemName.ShouldBe(systemName);
    }

    [Fact]
    public void AddTable_DatabaseTableNameAlreadyExistsException()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<DatabaseTableNameAlreadyExistsException>(() =>
        {
            source.AddTable(
                "Table",
                "TableS_Custom",
                [new CreateDatabaseColumn(DatabaseColumnKind.Unknown, "Column", "ColumnS")]);
        });
    }

    [Fact]
    public void AddTable_DatabaseTableSystemNameAlreadyExistsException()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<DatabaseTableSystemNameAlreadyExistsException>(() =>
        {
            source.AddTable(
                "Table_Custom",
                "TableS",
                [new CreateDatabaseColumn(DatabaseColumnKind.Unknown, "Column", "ColumnS")]);
        });
    }

    [Fact]
    public void AddTable_DatabaseColumnSystemNameAlreadyExistsException()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<DatabaseTableColumnsEmptyException>(() =>
        {
            source.AddTable("Table_Custom", "Table_Custom", []);
        });
    }

    [Theory]
    [InlineData("Table1", "_Table1_")]
    [InlineData("TableCustom", "Table__Custom__2")]
    [InlineData("TableSql", "Table__Sql_10")]
    public void UpdateTable(string name, string systemName)
    {
        var source = CreateDatabaseSource();

        var tableId = source.Tables[0].Id;

        source.UpdateTable(tableId, name, systemName);

        var table = source.GetTableById(tableId);

        table.Name.ShouldBe(name);
        table.SystemName.ShouldBe(systemName);
    }

    [Fact]
    public void UpdateTable_DatabaseTableNameAlreadyExistsException()
    {
        var source = CreateDatabaseSource();

        var tableId = source.Tables[0].Id;

        Assert.Throws<DatabaseTableNameAlreadyExistsException>(() =>
        {
            source.UpdateTable(tableId, "Table2", "Test");
        });
    }

    [Fact]
    public void UpdateTable_DatabaseTableSystemNameAlreadyExistsException()
    {
        var source = CreateDatabaseSource();

        var tableId = source.Tables[0].Id;

        Assert.Throws<DatabaseTableSystemNameAlreadyExistsException>(() =>
        {
            source.UpdateTable(tableId, "Test", "Table2S");
        });
    }

    [Fact]
    public void DeleteTable()
    {
        var source = CreateDatabaseSource();

        source.Tables.Count.ShouldBe(2);

        source.DeleteTable(source.Tables[0].Id);

        source.Tables.Count.ShouldBe(1);
    }

    [Fact]
    public void DeleteTable_DatabaseSourceTablesEmptyException()
    {
        var source = CreateDatabaseSource();

        source.Tables.Count.ShouldBe(2);

        source.DeleteTable(source.Tables[0].Id);

        source.Tables.Count.ShouldBe(1);

        Assert.Throws<DatabaseSourceTablesEmptyException>(() =>
        {
            source.DeleteTable(source.Tables[0].Id);
        });
    }

    private static DatabaseSource CreateDatabaseSource()
    {
        return new DatabaseSource(
            Guid.NewGuid(),
            null,
            DatabaseKind.SqlServer,
            "Test",
            "Connection",
            [
                new CreateDatabaseTable(
                    "Table",
                    "TableS",
                    [new CreateDatabaseColumn(DatabaseColumnKind.Unknown, "Column", "ColumnS")]),
                new CreateDatabaseTable(
                    "Table2",
                    "Table2S",
                    [new CreateDatabaseColumn(DatabaseColumnKind.Unknown, "Column", "ColumnS")])
            ]);
    }
}
