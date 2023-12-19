using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RajaFath.Migrations
{
    public partial class logo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoAddress",
                table: "siteSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoAddress",
                table: "siteSettings");
        }
    }
}
