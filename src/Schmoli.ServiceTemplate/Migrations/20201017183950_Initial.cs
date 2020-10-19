using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Schmoli.ServiceTemplate.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "SecondaryItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 32, nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", maxLength: 32, nullable: false),
                    SecondaryItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryItems_SecondaryItems_SecondaryItemId",
                        column: x => x.SecondaryItemId,
                        principalTable: "SecondaryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryItems_Name",
                table: "PrimaryItems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryItems_SecondaryItemId",
                table: "PrimaryItems",
                column: "SecondaryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryItems_Name",
                table: "SecondaryItems",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrimaryItems");

            migrationBuilder.DropTable(
                name: "SecondaryItems");
        }
    }
}
