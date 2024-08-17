using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteBlogs.Migrations
{
    /// <inheritdoc />
    public partial class Addlikesecondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogPostId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "BlogPosts");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostLikes_BlogpostId",
                table: "BlogPostLikes",
                column: "BlogpostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogpostId",
                table: "BlogPostLikes",
                column: "BlogpostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogpostId",
                table: "BlogPostLikes");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostLikes_BlogpostId",
                table: "BlogPostLikes");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogPostId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
