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

        var databaseSource1 = new DatabaseSource(
            guidGenerator.Create(),
            null,
            DatabaseKind.Postgresql,
            "Test_Duplicate",
            "Test",
            "Host=127.0.0.1;Port=55217;Database=postgres;Username=postgres;Password=postgres",
            [
                new CreateDatabaseTable(
                    "Table",
                    "Table",
                    [new CreateDatabaseColumn(DataSourceEntityPropertyKind.Uuid, "Column", "Column")]),
                new CreateDatabaseTable(
                    "TableX",
                    "TableX",
                    [new CreateDatabaseColumn(DataSourceEntityPropertyKind.Uuid, "ColumnX", "ColumnX")]),
            ]);

        databaseSource1.AddInternalRelation(
            DataSourceRelationDirection.OneToMany,
            "TableTX",
            new DataSourceInternalLink(
                databaseSource1.Tables[0].Id,
                databaseSource1.Tables[0].Columns[0].Id),
            new DataSourceInternalLink(
                databaseSource1.Tables[1].Id,
                databaseSource1.Tables[1].Columns[0].Id));

        await databaseSourceRepository.InsertAsync(databaseSource1);

        var databaseSource2 = new DatabaseSource(
            guidGenerator.Create(),
            null,
            DatabaseKind.MySql,
            "Test",
            "Pr",
            "Server=127.0.0.1;Port=63838;Database=test;Uid=mysql;Pwd=mysql",
            [
                new CreateDatabaseTable(
                    "Table",
                    "Table",
                    [new CreateDatabaseColumn(DataSourceEntityPropertyKind.Uuid, "Column", "Column")]),
                new CreateDatabaseTable(
                    "TableY",
                    "TableY",
                    [new CreateDatabaseColumn(DataSourceEntityPropertyKind.Uuid, "ColumnY", "ColumnY")]),
            ]);

        databaseSource2.AddInternalRelation(
            DataSourceRelationDirection.ManyToOne,
            "TablesY",
            new DataSourceInternalLink(
                databaseSource2.Tables[0].Id,
                databaseSource2.Tables[0].Columns[0].Id),
            new DataSourceInternalLink(
                databaseSource2.Tables[1].Id,
                databaseSource2.Tables[1].Columns[0].Id));

        await databaseSourceRepository.InsertAsync(databaseSource2);

        await uow.CompleteAsync();
    }
}
