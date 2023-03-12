using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfoTrackTest.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchEngine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultPageSize = table.Column<int>(type: "int", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchEngine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchEngineId = table.Column<int>(type: "int", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_SearchEngine_SearchEngineId",
                        column: x => x.SearchEngineId,
                        principalTable: "SearchEngine",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "SearchEngine",
                columns: new[] { "Id", "CreatedDateTime", "DefaultPageSize", "Name", "Url", "UserAgent" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 12, 12, 38, 1, 14, DateTimeKind.Utc).AddTicks(9992), null, "Google", "https://www.google.co.uk/search?num=##ResultNumber##&q=##SearchKeywords##", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36" },
                    { 2, new DateTime(2023, 3, 12, 12, 38, 1, 14, DateTimeKind.Utc).AddTicks(9997), null, "Bing", "https://www.bing.com/search?q=##SearchKeywords##&count=##ResultNumber##", "Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm) Chrome/W.X.Y.Z Safari/537.36" },
                    { 3, new DateTime(2023, 3, 12, 12, 38, 1, 15, DateTimeKind.Utc).AddTicks(28), 7, "Yahoo", "https://uk.search.yahoo.com/search?p=##SearchKeywords##&b=##Offset##&pz=##DefaultOffsetSize##", "Mozilla/5.0 (compatible; Yahoo! Slurp/3.0; http://help.yahoo.com/help/us/ysearch/slurp)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_SearchEngineId",
                table: "History",
                column: "SearchEngineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "SearchEngine");
        }
    }
}
