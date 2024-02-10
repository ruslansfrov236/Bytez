using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bytez.data.Migrations
{
    public partial class mg_22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "ContactWalls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ContactWalls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ContactCalls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "ContactWalls");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ContactWalls");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ContactCalls");
        }
    }
}
