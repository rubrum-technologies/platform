using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rubrum.Platform.DataSourceService.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RubrumDataSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Prefix = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ConnectionString = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubrumDataSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RubrumDatabaseTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DatabaseSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SystemName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Schema = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubrumDatabaseTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RubrumDatabaseTables_RubrumDataSources_DatabaseSourceId",
                        column: x => x.DatabaseSourceId,
                        principalTable: "RubrumDataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RubrumDataSourceInternalRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Left_EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Left_PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Right_EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Right_PropertyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubrumDataSourceInternalRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RubrumDataSourceInternalRelations_RubrumDataSources_DataSou~",
                        column: x => x.DataSourceId,
                        principalTable: "RubrumDataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RubrumDatabaseColumns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SystemName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    IsNotNull = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubrumDatabaseColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RubrumDatabaseColumns_RubrumDatabaseTables_TableId",
                        column: x => x.TableId,
                        principalTable: "RubrumDatabaseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RubrumDatabaseColumns_TableId",
                table: "RubrumDatabaseColumns",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_RubrumDatabaseTables_DatabaseSourceId",
                table: "RubrumDatabaseTables",
                column: "DatabaseSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RubrumDataSourceInternalRelations_DataSourceId",
                table: "RubrumDataSourceInternalRelations",
                column: "DataSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RubrumDatabaseColumns");

            migrationBuilder.DropTable(
                name: "RubrumDataSourceInternalRelations");

            migrationBuilder.DropTable(
                name: "RubrumDatabaseTables");

            migrationBuilder.DropTable(
                name: "RubrumDataSources");
        }
    }
}
