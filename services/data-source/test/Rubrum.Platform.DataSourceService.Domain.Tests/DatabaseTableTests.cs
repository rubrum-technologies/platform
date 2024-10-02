using Rubrum.Platform.DataSourceService.Database;
using Shouldly;
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
    public void SetName_MaxLenght()
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
    public void SetSystemName_MaxLenght()
    {
        var table = CreateDatabaseTable();

        Assert.Throws<ArgumentException>(() =>
        {
            table.SetSystemName(string.Empty.PadRight(DatabaseTableConstants.SystemNameLength + 1, '-'));
        });
    }

    private static DatabaseTable CreateDatabaseTable()
    {
        return new DatabaseTable(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Table",
            "TableS",
            [new CreateDatabaseColumn(DatabaseColumnKind.Unknown, "Column", "ColumnS")]);
    }
}
