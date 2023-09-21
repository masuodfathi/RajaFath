using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RajaFath.Data.Migrations
{
    public partial class modelOfPostImageComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Images",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Images",
                newName: "Caption");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Images",
                newName: "AltText");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeaturedImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(type: "int", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: true),
                    Dislikes = table.Column<int>(type: "int", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    AuthorBio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCommentEnabled = table.Column<bool>(type: "bit", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    WordCount = table.Column<int>(type: "int", nullable: true),
                    ReadingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OgTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OgDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledPublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsScheduled = table.Column<bool>(type: "bit", nullable: true),
                    ShareCount = table.Column<int>(type: "int", nullable: true),
                    BookmarkCount = table.Column<int>(type: "int", nullable: true),
                    CommentCount = table.Column<int>(type: "int", nullable: true),
                    ReportCount = table.Column<int>(type: "int", nullable: true),
                    IsVideo = table.Column<bool>(type: "bit", nullable: true),
                    IsAudio = table.Column<bool>(type: "bit", nullable: true),
                    IsImageGallery = table.Column<bool>(type: "bit", nullable: true),
                    ContentSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentAttribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageRating = table.Column<double>(type: "float", nullable: true),
                    TotalRatings = table.Column<int>(type: "int", nullable: true),
                    IsUserFavorite = table.Column<bool>(type: "bit", nullable: true),
                    FacebookLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    Upvotes = table.Column<int>(type: "int", nullable: true),
                    Downvotes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImagePosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagePosts_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostPost",
                columns: table => new
                {
                    RecommendedPostsId = table.Column<int>(type: "int", nullable: false),
                    RelatedPostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPost", x => new { x.RecommendedPostsId, x.RelatedPostsId });
                    table.ForeignKey(
                        name: "FK_PostPost_Posts_RecommendedPostsId",
                        column: x => x.RecommendedPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostPost_Posts_RelatedPostsId",
                        column: x => x.RelatedPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePosts_ImageId",
                table: "ImagePosts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePosts_PostId",
                table: "ImagePosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPost_RelatedPostsId",
                table: "PostPost",
                column: "RelatedPostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ImagePosts");

            migrationBuilder.DropTable(
                name: "PostPost");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Images",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "Caption",
                table: "Images",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AltText",
                table: "Images",
                newName: "Description");
        }
    }
}
