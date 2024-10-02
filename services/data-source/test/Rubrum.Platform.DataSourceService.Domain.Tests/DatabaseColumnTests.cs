using Rubrum.Platform.DataSourceService.Database;
using Shouldly;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public class DatabaseColumnTests
{
    [Theory]
    [InlineData("Column_Custom")]
    [InlineData("Column__W")]
    [InlineData("Custom")]
    public void SetName(string name)
    {
        var column = CreateDatabaseColumn();

        column.SetName(name);

        column.Name.ShouldBe(name);
    }

    [Fact]
    public void SetName_Empty()
    {
        var column = CreateDatabaseColumn();

        Assert.Throws<ArgumentException>(() => { column.SetName(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetName_MaxLength()
    {
        var column = CreateDatabaseColumn();

        Assert.Throws<ArgumentException>(() =>
        {
            column.SetName(string.Empty.PadRight(DatabaseColumnConstants.NameLength + 1, '-'));
        });
    }

    [Theory]
    [InlineData("Column_Sys")]
    [InlineData("Column__Q")]
    [InlineData("Custom_Sys")]
    public void SetSystemName(string systemName)
    {
        var column = CreateDatabaseColumn();

        column.SetSystemName(systemName);

        column.SystemName.ShouldBe(systemName);
    }

    [Fact]
    public void SetSystemName_Empty()
    {
        var column = CreateDatabaseColumn();

        Assert.Throws<ArgumentException>(() => { column.SetSystemName(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetSystemName_MaxLength()
    {
        var column = CreateDatabaseColumn();

        Assert.Throws<ArgumentException>(() =>
        {
            column.SetSystemName(string.Empty.PadRight(DatabaseColumnConstants.SystemNameLength + 1, '-'));
        });
    }

    private static DatabaseColumn CreateDatabaseColumn()
    {
        return new DatabaseColumn(Guid.NewGuid(), Guid.NewGuid(), DatabaseColumnKind.Number, "Column", "ColumnS");
    }
}
