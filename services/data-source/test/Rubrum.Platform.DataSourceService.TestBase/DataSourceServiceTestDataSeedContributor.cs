using Rubrum.Platform.DataSourceService.Database;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceServiceTestDataSeedContributor(
    IUnitOfWorkManager unitOfWorkManager,
    IGuidGenerator guidGenerator,
    IDatabaseSourceRepository databaseSourceRepository) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        await databaseSourceRepository.InsertAsync(new DatabaseSource(
            guidGenerator.Create(),
            null,
            DatabaseKind.SqlServer,
            "Test_Duplicate",
            "ConnectionTest",
            [
                new CreateDatabaseTable(
                    "Table",
                    "Table",
                    [new CreateDatabaseColumn(DatabaseColumnKind.Uuid, "Column", "Column")]),
            ]));

        await databaseSourceRepository.InsertAsync(new DatabaseSource(
            guidGenerator.Create(),
            null,
            DatabaseKind.MySql,
            "Test",
            "ConnectionTest",
            [
                new CreateDatabaseTable(
                    "Table",
                    "Table",
                    [new CreateDatabaseColumn(DatabaseColumnKind.Uuid, "Column", "Column")]),
            ]));

        await uow.CompleteAsync();
    }
}
