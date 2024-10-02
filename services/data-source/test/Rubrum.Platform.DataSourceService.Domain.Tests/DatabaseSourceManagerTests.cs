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
            "Connection",
            [
                new CreateDatabaseTable(
                    "Table",
                    "TableS",
                    [new CreateDatabaseColumn(DatabaseColumnKind.String, "Column", "ColumnS")]),
            ],
            "PR");

        source.ShouldNotBeNull();
        source.Kind.ShouldBe(DatabaseKind.Postgresql);
        source.Name.ShouldBe("Test2");
        source.ConnectionString.ShouldBe("Connection");
        source.Tables.Count.ShouldBe(1);
        source.Tables[0].Name.ShouldBe("Table");
        source.Tables[0].SystemName.ShouldBe("TableS");
        source.Tables[0].Columns.Count.ShouldBe(1);
        source.Tables[0].Columns[0].Kind.ShouldBe(DatabaseColumnKind.String);
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
                "Connection",
                [
                    new CreateDatabaseTable(
                        "Table",
                        "TableS",
                        [new CreateDatabaseColumn(DatabaseColumnKind.String, "Column", "ColumnS")]),
                ],
                "PR");
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
}
