using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RajaFath.Migrations
{
    public partial class onModelPostImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_FeaturedImageId",
                table: "Posts",
                column: "FeaturedImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_FeaturedImageId",
                table: "Posts",
                column: "FeaturedImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Images_FeaturedImageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_FeaturedImageId",
                table: "Posts");
        }
    }
}
