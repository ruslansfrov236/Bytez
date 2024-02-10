using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bytez.data.Migrations
{
    public partial class mg_16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoPath",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isVideo",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoPath",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "isVideo",
                table: "Blogs");
        }
    }
}
