using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class EventUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventPackagesDetail_EventPackageDetailId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventPackagesDetail");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventPackageDetailId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventPackageDetailId",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "EventPackagesType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatingArragement = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPackagesType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventPackages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Placement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventPackages_EventPackagesType_PackageTypeId",
                        column: x => x.PackageTypeId,
                        principalTable: "EventPackagesType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPackages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPackages_EventId",
                table: "EventPackages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPackages_PackageTypeId",
                table: "EventPackages",
                column: "PackageTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventPackages");

            migrationBuilder.DropTable(
                name: "EventPackagesType");

            migrationBuilder.AddColumn<string>(
                name: "EventPackageDetailId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventPackagesDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SeatingArragement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPackagesDetail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventPackageDetailId",
                table: "Events",
                column: "EventPackageDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventPackagesDetail_EventPackageDetailId",
                table: "Events",
                column: "EventPackageDetailId",
                principalTable: "EventPackagesDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
