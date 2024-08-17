using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteBlogs.Migrations
{
    /// <inheritdoc />
    public partial class Addinglikefunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BlogPostId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogPostLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogpostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostLikes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogPostId",
                table: "BlogPosts",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPosts_BlogPostId",
                table: "BlogPosts",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropTable(
                name: "BlogPostLikes");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "BlogPosts");
        }
    }
}
