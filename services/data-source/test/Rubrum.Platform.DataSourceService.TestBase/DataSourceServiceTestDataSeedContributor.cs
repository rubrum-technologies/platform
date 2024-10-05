using Rubrum.Platform.DataSourceService.Database;
using Testcontainers.PostgreSql;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceServiceTestDataSeedContributor(
    PostgreSqlContainer postgreSqlContainer,
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

        var databaseSourceTest = new DatabaseSource(
            guidGenerator.Create(),
            null,
            DatabaseKind.Postgresql,
            "West",
            "West",
            postgreSqlContainer.GetConnectionString(),
            [
                new CreateDatabaseTable(
                    "Source",
                    "RubrumDataSources",
                    [
                        new CreateDatabaseColumn(DataSourceEntityPropertyKind.Uuid, "Identifier", "Id"),
                        new CreateDatabaseColumn(DataSourceEntityPropertyKind.String, "Naming", "Name"),
                    ]),
                new CreateDatabaseTable(
                    "Table",
                    "RubrumDatabaseTables",
                    [
                        new CreateDatabaseColumn(DataSourceEntityPropertyKind.Uuid, "Identifier", "Id"),
                        new CreateDatabaseColumn(DataSourceEntityPropertyKind.String, "Naming", "Name"),
                        new CreateDatabaseColumn(DataSourceEntityPropertyKind.String, "SystemName", "SystemName"),
                        new CreateDatabaseColumn(
                            DataSourceEntityPropertyKind.Uuid,
                            "DatabaseSourceId",
                            "DatabaseSourceId"),
                    ]),
            ]);

        databaseSourceTest.AddInternalRelation(
            DataSourceRelationDirection.OneToMany,
            "Tables",
            new DataSourceInternalLink(
                databaseSourceTest.Entities[0].Id,
                databaseSourceTest.Entities[0].Properties[0].Id),
            new DataSourceInternalLink(
                databaseSourceTest.Entities[1].Id,
                databaseSourceTest.Entities[1].Properties[3].Id));

        await databaseSourceRepository.InsertAsync(databaseSourceTest);

        await uow.CompleteAsync();
    }
}
