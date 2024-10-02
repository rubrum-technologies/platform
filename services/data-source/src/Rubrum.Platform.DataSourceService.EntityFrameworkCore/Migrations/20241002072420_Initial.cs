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
                name: "Rubrum.PlatformDataSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Prefix = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ConnectionString = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
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
                    table.PrimaryKey("PK_Rubrum.PlatformDataSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rubrum.PlatformDatabaseTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DatabaseSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SystemName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubrum.PlatformDatabaseTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rubrum.PlatformDatabaseTables_Rubrum.PlatformDataSources_Da~",
                        column: x => x.DatabaseSourceId,
                        principalTable: "Rubrum.PlatformDataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rubrum.PlatformDatabaseColumns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SystemName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubrum.PlatformDatabaseColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rubrum.PlatformDatabaseColumns_Rubrum.PlatformDatabaseTable~",
                        column: x => x.TableId,
                        principalTable: "Rubrum.PlatformDatabaseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rubrum.PlatformDatabaseColumns_TableId",
                table: "Rubrum.PlatformDatabaseColumns",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Rubrum.PlatformDatabaseTables_DatabaseSourceId",
                table: "Rubrum.PlatformDatabaseTables",
                column: "DatabaseSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rubrum.PlatformDatabaseColumns");

            migrationBuilder.DropTable(
                name: "Rubrum.PlatformDatabaseTables");

            migrationBuilder.DropTable(
                name: "Rubrum.PlatformDataSources");
        }
    }
}
