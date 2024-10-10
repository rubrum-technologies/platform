using Rubrum.Platform.DataSourceService.Database;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public class DatabaseTableTests
{
    [Fact]
    public void Create_DatabaseTableColumnsEmptyException()
    {
        Assert.Throws<DatabaseTableColumnsEmptyException>(() =>
            new DatabaseTable(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Table",
                "TableS",
                []));
    }

    [Theory]
    [InlineData("Table_Custom")]
    [InlineData("Table__W")]
    [InlineData("Custom")]
    public void SetName(string name)
    {
        var table = CreateDatabaseTable();

        table.SetName(name);

        table.Name.ShouldBe(name);
    }

    [Fact]
    public void SetName_Empty()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<ArgumentException>(() => { table.SetName(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetName_MaxLength()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<ArgumentException>(() =>
        {
            table.SetName(string.Empty.PadRight(DatabaseTableConstants.NameLength + 1, '-'));
        });
    }

    [Theory]
    [InlineData("Table_Sys")]
    [InlineData("Table__Q")]
    [InlineData("Custom_Sys")]
    public void SetSystemName(string systemName)
    {
        var table = CreateDatabaseTable();

        table.SetSystemName(systemName);

        table.SystemName.ShouldBe(systemName);
    }

    [Fact]
    public void SetSystemName_Empty()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<ArgumentException>(() => { table.SetSystemName(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetSystemName_MaxLength()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<ArgumentException>(() =>
        {
            table.SetSystemName(string.Empty.PadRight(DatabaseTableConstants.SystemNameLength + 1, '-'));
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetColumnById(int columnIndex)
    {
        var table = CreateDatabaseTable();

        var columnId = table.Columns[columnIndex].Id;

        var column = table.GetColumnById(columnId);

        column.ShouldNotBeNull();
        column.Id.ShouldBe(columnId);
    }

    [Fact]
    public void GetColumnById_EntityNotFoundException()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<EntityNotFoundException>(() => { table.GetColumnById(Guid.NewGuid()); });
    }

    [Theory]
    [InlineData(DataSourceEntityPropertyKind.Unknown, "Three", "Test10")]
    [InlineData(DataSourceEntityPropertyKind.Float, "One", "Test19")]
    [InlineData(DataSourceEntityPropertyKind.Int, "TestQW", "Test18")]
    [InlineData(DataSourceEntityPropertyKind.String, "TestNDE", "Test14")]
    [InlineData(DataSourceEntityPropertyKind.DateTime, "TestD", "Test156")]
    [InlineData(DataSourceEntityPropertyKind.Uuid, "TestB", "Test50")]
    [InlineData(DataSourceEntityPropertyKind.Unknown, "TestQ", "Test80")]
    public void AddColumn(DataSourceEntityPropertyKind kind, string name, string systemName)
    {
        var table = CreateDatabaseTable();

        table.Columns.Count.ShouldBe(4);

        var column = table.AddColumn(Guid.NewGuid(), kind, name, systemName);

        table.Columns.Count.ShouldBe(5);

        column.Kind.ShouldBe(kind);
        column.Name.ShouldBe(name);
        column.SystemName.ShouldBe(systemName);
    }

    [Theory]
    [InlineData(DataSourceEntityPropertyKind.String, "Column", "Test10")]
    [InlineData(DataSourceEntityPropertyKind.Float, "Column2", "Test19")]
    [InlineData(DataSourceEntityPropertyKind.Int, "Column3", "Test18")]
    [InlineData(DataSourceEntityPropertyKind.DateTime, "Column4", "Test18")]
    public void AddColumn_DatabaseColumnNameAlreadyExistsException(
        DataSourceEntityPropertyKind kind,
        string name,
        string systemName)
    {
        var table = CreateDatabaseTable();

        Assert.Throws<DatabaseColumnNameAlreadyExistsException>(() =>
        {
            table.AddColumn(Guid.NewGuid(), kind, name, systemName);
        });
    }

    [Theory]
    [InlineData(DataSourceEntityPropertyKind.String, "ColumnQ", "ColumnS")]
    [InlineData(DataSourceEntityPropertyKind.Float, "ColumnQ2", "Column1")]
    [InlineData(DataSourceEntityPropertyKind.Int, "ColumnQ3", "Column2")]
    [InlineData(DataSourceEntityPropertyKind.DateTime, "ColumnQ4", "Column3")]
    public void AddColumn_DatabaseColumnSystemNameAlreadyExistsException(
        DataSourceEntityPropertyKind kind,
        string name,
        string systemName)
    {
        var table = CreateDatabaseTable();

        Assert.Throws<DatabaseColumnSystemNameAlreadyExistsException>(() =>
        {
            table.AddColumn(Guid.NewGuid(), kind, name, systemName);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void UpdateColumn(int columnIndex)
    {
        var table = CreateDatabaseTable();

        var columnId = table.Columns[columnIndex].Id;

        var column = table.UpdateColumn(columnId, "Test", "TestS");

        column.ShouldNotBeNull();

        column.Id.ShouldBe(columnId);
        column.Name.ShouldBe("Test");
        column.SystemName.ShouldBe("TestS");
    }

    [Theory]
    [InlineData(3, "Column", "Test10")]
    [InlineData(2, "Column2", "Test19")]
    [InlineData(1, "Column3", "Test18")]
    [InlineData(0, "Column4", "Test18")]
    public void UpdateColumn_DatabaseColumnNameAlreadyExistsException(
        int columnIndex,
        string name,
        string systemName)
    {
        var table = CreateDatabaseTable();

        var column = table.Columns[columnIndex];

        Assert.Throws<DatabaseColumnNameAlreadyExistsException>(() =>
        {
            table.UpdateColumn(column.Id, name, systemName);
        });
    }

    [Theory]
    [InlineData(2, "Test1", "ColumnS")]
    [InlineData(3, "Test2", "Column1")]
    [InlineData(0, "Test3", "Column2")]
    [InlineData(1, "Test5", "Column3")]
    public void UpdateColumn_DatabaseColumnSystemNameAlreadyExistsException(
        int columnIndex,
        string name,
        string systemName)
    {
        var table = CreateDatabaseTable();

        var column = table.Columns[columnIndex];

        Assert.Throws<DatabaseColumnSystemNameAlreadyExistsException>(() =>
        {
            table.UpdateColumn(column.Id, name, systemName);
        });
    }

    [Fact]
    public void UpdateColumn_EntityNotFoundException()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<EntityNotFoundException>(() => { table.UpdateColumn(Guid.NewGuid(), "Test1", "Test1"); });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void DeleteColumn(int columnIndex)
    {
        var table = CreateDatabaseTable();

        table.Columns.Count.ShouldBe(4);

        var columnId = table.Columns[columnIndex].Id;

        table.DeleteColumn(columnId);

        table.Columns.Count.ShouldBe(3);
    }

    [Fact]
    public void DeleteColumn_EntityNotFoundException()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<EntityNotFoundException>(() => { table.DeleteColumn(Guid.NewGuid()); });
    }

    [Fact]
    public void DeleteColumn_DatabaseTableColumnsEmptyException()
    {
        var table = CreateDatabaseTable();

        table.Columns.Count.ShouldBe(4);

        var columnId1 = table.Columns[0].Id;
        var columnId2 = table.Columns[1].Id;
        var columnId3 = table.Columns[2].Id;
        var columnId4 = table.Columns[3].Id;

        table.DeleteColumn(columnId1);
        table.DeleteColumn(columnId2);
        table.DeleteColumn(columnId3);

        Assert.Throws<DatabaseTableColumnsEmptyException>(() => { table.DeleteColumn(columnId4); });
    }

    private static DatabaseTable CreateDatabaseTable()
    {
        return new DatabaseTable(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Table",
            "TableS",
            [
                new CreateDatabaseColumn(Guid.NewGuid(), DataSourceEntityPropertyKind.Unknown, "Column", "ColumnS"),
                new CreateDatabaseColumn(Guid.NewGuid(), DataSourceEntityPropertyKind.String, "Column2", "Column1"),
                new CreateDatabaseColumn(Guid.NewGuid(), DataSourceEntityPropertyKind.DateTime, "Column3", "Column2"),
                new CreateDatabaseColumn(Guid.NewGuid(), DataSourceEntityPropertyKind.Int, "Column4", "Column3"),
            ]);
    }
}
