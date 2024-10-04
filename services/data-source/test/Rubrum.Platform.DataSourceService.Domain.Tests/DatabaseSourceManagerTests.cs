using Rubrum.Platform.DataSourceService.Database;
using Shouldly;
using Volo.Abp.Uow;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public sealed class DatabaseSourceManagerTests : DataSourceServiceDomainTestBase
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly DatabaseSourceManager _manager;
    private readonly IDatabaseSourceRepository _repository;

    public DatabaseSourceManagerTests()
    {
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        _manager = GetRequiredService<DatabaseSourceManager>();
        _repository = GetRequiredService<IDatabaseSourceRepository>();
    }

    [Fact]
    public async Task CreateAsync()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var source = await _manager.CreateAsync(
            DatabaseKind.Postgresql,
            "Test2",
            "Qa",
            "Connection",
            [
                new CreateDatabaseTable(
                    "Table",
                    "TableS",
                    [new CreateDatabaseColumn(DataSourceEntityPropertyKind.String, "Column", "ColumnS")]),
            ]);

        source.ShouldNotBeNull();
        source.Kind.ShouldBe(DatabaseKind.Postgresql);
        source.Name.ShouldBe("Test2");
        source.ConnectionString.ShouldBe("Connection");
        source.Tables.Count.ShouldBe(1);
        source.Tables[0].Name.ShouldBe("Table");
        source.Tables[0].SystemName.ShouldBe("TableS");
        source.Tables[0].Columns.Count.ShouldBe(1);
        source.Tables[0].Columns[0].Kind.ShouldBe(DataSourceEntityPropertyKind.String);
        source.Tables[0].Columns[0].Name.ShouldBe("Column");
        source.Tables[0].Columns[0].SystemName.ShouldBe("ColumnS");
    }

    [Fact]
    public async Task CreateAsync_DataSourceNameAlreadyExistsException()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        await Assert.ThrowsAsync<DataSourceNameAlreadyExistsException>(async () =>
        {
            await _manager.CreateAsync(
                DatabaseKind.Postgresql,
                "Test",
                "Sa",
                "Connection",
                [
                    new CreateDatabaseTable(
                        "Table",
                        "TableS",
                        [new CreateDatabaseColumn(DataSourceEntityPropertyKind.String, "Column", "ColumnS")]),
                ]);
        });
    }

    [Fact]
    public async Task CreateAsync_DataSourcePrefixAlreadyExistsException()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        await Assert.ThrowsAsync<DataSourcePrefixAlreadyExistsException>(async () =>
        {
            await _manager.CreateAsync(
                DatabaseKind.Postgresql,
                "TestZxc",
                "Pr",
                "Connection",
                [
                    new CreateDatabaseTable(
                        "Table",
                        "TableS",
                        [new CreateDatabaseColumn(DataSourceEntityPropertyKind.String, "Column", "ColumnS")]),
                ]);
        });
    }

    [Fact]
    public async Task ChangeNameAsync()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var source = await _repository.GetAsync(x => x.Name == "Test");

        await _manager.ChangeNameAsync(source, "TestChangeName");

        source.ShouldNotBeNull();
        source.Name.ShouldBe("TestChangeName");
    }

    [Fact]
    public async Task ChangeNameAsync_DataSourceNameAlreadyExistsException()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var source = await _repository.GetAsync(x => x.Name == "Test");

        await Assert.ThrowsAsync<DataSourceNameAlreadyExistsException>(async () =>
        {
            await _manager.ChangeNameAsync(source, "Test_Duplicate");
        });
    }

    [Fact]
    public async Task ChangePrefixAsync()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var source = await _repository.GetAsync(x => x.Name == "Test");

        await _manager.ChangePrefixAsync(source, "Prefix");

        source.ShouldNotBeNull();
        source.Prefix.ShouldBe("Prefix");
    }

    [Fact]
    public async Task ChangePrefixAsync_DataSourcePrefixAlreadyExistsException()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var source = await _repository.GetAsync(x => x.Name == "Test");

        await Assert.ThrowsAsync<DataSourcePrefixAlreadyExistsException>(async () =>
        {
            await _manager.ChangePrefixAsync(source, "Test");
        });
    }
}
