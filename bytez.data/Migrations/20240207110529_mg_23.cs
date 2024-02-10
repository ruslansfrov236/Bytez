using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bytez.data.Migrations
{
    public partial class mg_23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_ContactWalls_ContactWallId",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Emails_ContactWallId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "ContactWallId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "ContactWalls");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactWallId",
                table: "Emails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "ContactWalls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ContactWallId",
                table: "Emails",
                column: "ContactWallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_ContactWalls_ContactWallId",
                table: "Emails",
                column: "ContactWallId",
                principalTable: "ContactWalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
