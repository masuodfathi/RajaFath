using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RajaFath.Data.Migrations
{
    public partial class pathfiels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Images");
        }
    }
}
