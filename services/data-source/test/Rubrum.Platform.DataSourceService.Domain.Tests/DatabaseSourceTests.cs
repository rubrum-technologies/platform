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
    public void SetName_MaxLength()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<ArgumentException>(() =>
        {
            source.SetName(string.Empty.PadRight(DataSourceConstants.NameLength + 1, '-'));
        });
    }

    [Theory]
    [InlineData("PR")]
    [InlineData("Qsf")]
    [InlineData("Zxc")]
    public void SetPrefix(string name)
    {
        var source = CreateDatabaseSource();

        source.SetPrefix(name);

        source.Prefix.ShouldBe(name);
    }

    [Fact]
    public void SetPrefix_Empty()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<ArgumentException>(() => { source.SetPrefix(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetPrefix_MaxLength()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<ArgumentException>(() =>
        {
            source.SetPrefix(string.Empty.PadRight(DataSourceConstants.NameLength + 1, 'Z'));
        });
    }

    [Theory]
    [InlineData("Qwe1")]
    [InlineData("Zxc-")]
    [InlineData("asd*")]
    [InlineData("zxcvq/")]
    [InlineData("/qwe/")]
    [InlineData(".asd.")]
    [InlineData(",w2")]
    [InlineData("^%@#$")]
    [InlineData("!QWE")]
    [InlineData("(asd)")]
    [InlineData("[ZXVC]")]
    [InlineData("asd\nasd")]
    [InlineData("xxx xxx")]
    public void SetPrefix_Regex(string prefix)
    {
        var source = CreateDatabaseSource();

        Assert.Throws<ArgumentException>(() => { source.SetPrefix(prefix); });
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
            Guid.NewGuid(),
            name,
            systemName,
            [
                new CreateDatabaseColumn(
                    Guid.NewGuid(),
                    DataSourceEntityPropertyKind.Unknown,
                    "Column",
                    "ColumnS")
            ]);

        table.DatabaseSourceId.ShouldBe(source.Id);
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
                Guid.NewGuid(),
                "Table",
                "TableS_Custom",
                [
                    new CreateDatabaseColumn(
                        Guid.NewGuid(),
                        DataSourceEntityPropertyKind.Unknown,
                        "Column",
                        "ColumnS")
                ]);
        });
    }

    [Fact]
    public void AddTable_DatabaseTableSystemNameAlreadyExistsException()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<DatabaseTableSystemNameAlreadyExistsException>(() =>
        {
            source.AddTable(
                Guid.NewGuid(),
                "Table_Custom",
                "TableS",
                [
                    new CreateDatabaseColumn(
                        Guid.NewGuid(),
                        DataSourceEntityPropertyKind.Unknown,
                        "Column",
                        "ColumnS")
                ]);
        });
    }

    [Fact]
    public void AddTable_DatabaseColumnSystemNameAlreadyExistsException()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<DatabaseTableColumnsEmptyException>(() =>
        {
            source.AddTable(Guid.NewGuid(), "Table_Custom", "Table_Custom", []);
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

        Assert.Throws<DatabaseSourceTablesEmptyException>(() => { source.DeleteTable(source.Tables[0].Id); });
    }

    [Theory]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 0, 1, 0)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 0, 0, 0)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 0, 1, 1)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 0, 0, 1)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 1, 1, 0)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 1, 0, 0)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 1, 1, 1)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 1, 0, 1)]
    public void AddRelation(
        DataSourceRelationDirection direction,
        int leftTableIndex,
        int leftColumnIndex,
        int rightTableIndex,
        int rightColumnIndex)
    {
        var source = CreateDatabaseSource();

        source.InternalRelations.Count.ShouldBe(0);

        var relation = source.AddInternalRelation(
            direction,
            "B",
            new DataSourceInternalLink(
                source.Tables[leftTableIndex].Id,
                source.Tables[leftTableIndex].Columns[leftColumnIndex].Id),
            new DataSourceInternalLink(
                source.Tables[rightTableIndex].Id,
                source.Tables[rightTableIndex].Columns[rightColumnIndex].Id));

        source.InternalRelations.Count.ShouldBe(1);

        relation.Direction.ShouldBe(direction);
        relation.Left.EntityId.ShouldBe(source.Tables[leftTableIndex].Id);
        relation.Left.PropertyId.ShouldBe(source.Tables[leftTableIndex].Columns[leftColumnIndex].Id);
        relation.Right.EntityId.ShouldBe(source.Tables[rightTableIndex].Id);
        relation.Right.PropertyId.ShouldBe(source.Tables[rightTableIndex].Columns[rightColumnIndex].Id);
    }

    [Theory]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 0, 1, 0)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 0, 0, 0)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 0, 1, 1)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 0, 0, 1)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 1, 1, 0)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 1, 0, 0)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 1, 1, 1)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 1, 0, 1)]
    public void AddRelation_DataSourceInternalRelationAlreadyExistsException(
        DataSourceRelationDirection direction,
        int leftTableIndex,
        int leftColumnIndex,
        int rightTableIndex,
        int rightColumnIndex)
    {
        var source = CreateDatabaseSource();

        source.InternalRelations.Count.ShouldBe(0);

        source.AddInternalRelation(
            direction,
            "X",
            new DataSourceInternalLink(
                source.Tables[leftTableIndex].Id,
                source.Tables[leftTableIndex].Columns[leftColumnIndex].Id),
            new DataSourceInternalLink(
                source.Tables[rightTableIndex].Id,
                source.Tables[rightTableIndex].Columns[rightColumnIndex].Id));

        Assert.Throws<DataSourceInternalRelationAlreadyExistsException>(() =>
        {
            source.AddInternalRelation(
                DataSourceRelationDirection.ManyToOne,
                "We",
                new DataSourceInternalLink(
                    source.Tables[leftTableIndex].Id,
                    source.Tables[leftTableIndex].Columns[leftColumnIndex].Id),
                new DataSourceInternalLink(
                    source.Tables[rightTableIndex].Id,
                    source.Tables[rightTableIndex].Columns[rightColumnIndex].Id));
        });
    }

    [Theory]
    [InlineData("TestError")]
    [InlineData("Test")]
    [InlineData("Tables")]
    [InlineData("Obj")]
    [InlineData("Cities")]
    [InlineData("Stores")]
    public void AddRelation_DataSourceInternalRelationNameAlreadyExistsException(string relationName)
    {
        var source = CreateDatabaseSource();

        source.InternalRelations.Count.ShouldBe(0);

        source.AddInternalRelation(
            DataSourceRelationDirection.OneToMany,
            relationName,
            new DataSourceInternalLink(
                source.Tables[0].Id,
                source.Tables[0].Columns[0].Id),
            new DataSourceInternalLink(
                source.Tables[1].Id,
                source.Tables[1].Columns[0].Id));

        Assert.Throws<DataSourceInternalRelationNameAlreadyExistsException>(() =>
        {
            source.AddInternalRelation(
                DataSourceRelationDirection.OneToMany,
                relationName,
                new DataSourceInternalLink(
                    source.Tables[0].Id,
                    source.Tables[0].Columns[1].Id),
                new DataSourceInternalLink(
                    source.Tables[1].Id,
                    source.Tables[1].Columns[1].Id));
        });
    }

    [Theory]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 0, 1, 0)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 0, 0, 0)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 0, 1, 1)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 0, 0, 1)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 1, 1, 0)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 1, 0, 0)]
    [InlineData(DataSourceRelationDirection.OneToMany, 0, 1, 1, 1)]
    [InlineData(DataSourceRelationDirection.ManyToOne, 1, 1, 0, 1)]
    public void DeleteRelation(
        DataSourceRelationDirection direction,
        int leftTableIndex,
        int leftColumnIndex,
        int rightTableIndex,
        int rightColumnIndex)
    {
        var source = CreateDatabaseSource();

        source.InternalRelations.Count.ShouldBe(0);

        var relation = source.AddInternalRelation(
            direction,
            "Ri",
            new DataSourceInternalLink(
                source.Tables[leftTableIndex].Id,
                source.Tables[leftTableIndex].Columns[leftColumnIndex].Id),
            new DataSourceInternalLink(
                source.Tables[rightTableIndex].Id,
                source.Tables[rightTableIndex].Columns[rightColumnIndex].Id));

        source.DeleteInternalRelation(relation.Id);

        source.InternalRelations.Count.ShouldBe(0);
    }

    [Fact]
    public void DeleteRelation_EntityNotFoundException()
    {
        var source = CreateDatabaseSource();

        Assert.Throws<EntityNotFoundException>(() =>
        {
            source.AddInternalRelation(
                DataSourceRelationDirection.ManyToOne,
                "Z",
                new DataSourceInternalLink(
                    Guid.NewGuid(),
                    source.Tables[0].Columns[0].Id),
                new DataSourceInternalLink(
                    source.Tables[1].Id,
                    source.Tables[1].Columns[0].Id));
        });

        Assert.Throws<EntityNotFoundException>(() =>
        {
            source.AddInternalRelation(
                DataSourceRelationDirection.ManyToOne,
                "B",
                new DataSourceInternalLink(
                    source.Tables[0].Id,
                    Guid.NewGuid()),
                new DataSourceInternalLink(
                    source.Tables[1].Id,
                    source.Tables[1].Columns[0].Id));
        });

        Assert.Throws<EntityNotFoundException>(() =>
        {
            source.AddInternalRelation(
                DataSourceRelationDirection.ManyToOne,
                "Test",
                new DataSourceInternalLink(
                    source.Tables[0].Id,
                    source.Tables[0].Columns[0].Id),
                new DataSourceInternalLink(
                    Guid.NewGuid(),
                    source.Tables[1].Columns[0].Id));
        });

        Assert.Throws<EntityNotFoundException>(() =>
        {
            source.AddInternalRelation(
                DataSourceRelationDirection.ManyToOne,
                "Test2",
                new DataSourceInternalLink(
                    source.Tables[0].Id,
                    source.Tables[0].Columns[0].Id),
                new DataSourceInternalLink(
                    source.Tables[1].Id,
                    Guid.NewGuid()));
        });
    }

    private static DatabaseSource CreateDatabaseSource()
    {
        return new DatabaseSource(
            Guid.NewGuid(),
            null,
            DatabaseKind.SqlServer,
            "Test",
            "Za",
            "Connection",
            [
                new CreateDatabaseTable(
                    Guid.NewGuid(),
                    "Table",
                    "TableS",
                    [
                        new CreateDatabaseColumn(
                            Guid.NewGuid(),
                            DataSourceEntityPropertyKind.Unknown,
                            "Column",
                            "ColumnS"),
                        new CreateDatabaseColumn(
                            Guid.NewGuid(),
                            DataSourceEntityPropertyKind.Unknown,
                            "ColumnX",
                            "ColumnX"),
                    ]),
                new CreateDatabaseTable(
                    Guid.NewGuid(),
                    "Table2",
                    "Table2S",
                    [
                        new CreateDatabaseColumn(
                            Guid.NewGuid(),
                            DataSourceEntityPropertyKind.Unknown,
                            "Column",
                            "ColumnS"),
                        new CreateDatabaseColumn(
                            Guid.NewGuid(),
                            DataSourceEntityPropertyKind.Unknown,
                            "ColumnQ",
                            "ColumnQ"),
                    ]),
            ]);
    }
}
