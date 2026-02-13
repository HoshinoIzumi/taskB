using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "Completed", "CreatedAt", "Description", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("61ac4ecd-adf2-425b-a48f-a2c02a8b5abe"), false, new DateTime(2026, 2, 12, 16, 4, 48, 202, DateTimeKind.Utc).AddTicks(1010), null, "Learn EF Core", null },
                    { new Guid("b0a1b050-7de8-42b0-9fcd-ae0375c0d996"), false, new DateTime(2026, 2, 12, 16, 4, 48, 202, DateTimeKind.Utc).AddTicks(1010), null, "Build a Web API", null },
                    { new Guid("c4fa81cc-ceef-402f-978b-c50e35e09b82"), true, new DateTime(2026, 2, 12, 16, 4, 48, 202, DateTimeKind.Utc).AddTicks(1000), null, "Learn .NET 8", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
