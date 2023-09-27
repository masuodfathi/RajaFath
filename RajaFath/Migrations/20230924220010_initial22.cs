using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RajaFath.Migrations
{
    public partial class initial22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Categories_CategoryId",
                table: "PostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_Posts_PostId",
                table: "PostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategory");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "postTags");

            migrationBuilder.RenameTable(
                name: "PostCategory",
                newName: "postCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagId",
                table: "postTags",
                newName: "IX_postTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_PostId",
                table: "postTags",
                newName: "IX_postTags_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategory_PostId",
                table: "postCategories",
                newName: "IX_postCategories_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategory_CategoryId",
                table: "postCategories",
                newName: "IX_postCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_postTags",
                table: "postTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_postCategories",
                table: "postCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_postCategories_Categories_CategoryId",
                table: "postCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_postCategories_Posts_PostId",
                table: "postCategories",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_postTags_Posts_PostId",
                table: "postTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_postTags_Tags_TagId",
                table: "postTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_postCategories_Categories_CategoryId",
                table: "postCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_postCategories_Posts_PostId",
                table: "postCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_postTags_Posts_PostId",
                table: "postTags");

            migrationBuilder.DropForeignKey(
                name: "FK_postTags_Tags_TagId",
                table: "postTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_postTags",
                table: "postTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_postCategories",
                table: "postCategories");

            migrationBuilder.RenameTable(
                name: "postTags",
                newName: "PostTag");

            migrationBuilder.RenameTable(
                name: "postCategories",
                newName: "PostCategory");

            migrationBuilder.RenameIndex(
                name: "IX_postTags_TagId",
                table: "PostTag",
                newName: "IX_PostTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_postTags_PostId",
                table: "PostTag",
                newName: "IX_PostTag_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_postCategories_PostId",
                table: "PostCategory",
                newName: "IX_PostCategory_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_postCategories_CategoryId",
                table: "PostCategory",
                newName: "IX_PostCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Categories_CategoryId",
                table: "PostCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_Posts_PostId",
                table: "PostCategory",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
