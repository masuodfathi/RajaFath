using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RajaFath.Migrations
{
    public partial class ChangeFeatureImaeTypeToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedImage",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "FeaturedImageId",
                table: "Posts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedImageId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "FeaturedImage",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
