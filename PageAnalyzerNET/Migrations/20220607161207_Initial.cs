using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PageAnalyzerNET.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusCode = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    H1 = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UrlId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlChecks_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlChecks_UrlId",
                table: "UrlChecks",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_Urls_Name",
                table: "Urls",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlChecks");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
