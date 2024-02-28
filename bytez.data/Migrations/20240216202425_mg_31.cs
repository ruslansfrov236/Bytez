using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bytez.data.Migrations
{
    public partial class mg_31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "OrderComponents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "OrderComponents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderComponents_AppUserId1",
                table: "OrderComponents",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderComponents_AspNetUsers_AppUserId1",
                table: "OrderComponents",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderComponents_AspNetUsers_AppUserId1",
                table: "OrderComponents");

            migrationBuilder.DropIndex(
                name: "IX_OrderComponents_AppUserId1",
                table: "OrderComponents");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "OrderComponents");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "OrderComponents");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    LocalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AppUserId1",
                table: "Locations",
                column: "AppUserId1");
        }
    }
}
