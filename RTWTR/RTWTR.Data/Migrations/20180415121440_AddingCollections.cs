using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RTWTR.Data.Migrations
{
    public partial class AddingCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTweets_AspNetUsers_UserId",
                table: "UserTweets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTweets",
                newName: "TwitterUserId");

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TwitterUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProfileImageUrl = table.Column<string>(nullable: true),
                    ScreenName = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitterUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionTweet",
                columns: table => new
                {
                    TweetId = table.Column<string>(nullable: false),
                    CollectionId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetCollection", x => new { x.TweetId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_TweetCollection_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetCollection_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetCollection_CollectionId",
                table: "CollectionTweet",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTweets_TwitterUser_TwitterUserId",
                table: "UserTweets",
                column: "TwitterUserId",
                principalTable: "TwitterUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTweets_TwitterUser_TwitterUserId",
                table: "UserTweets");

            migrationBuilder.DropTable(
                name: "CollectionTweet");

            migrationBuilder.DropTable(
                name: "TwitterUser");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.RenameColumn(
                name: "TwitterUserId",
                table: "UserTweets",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTweets_AspNetUsers_UserId",
                table: "UserTweets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
