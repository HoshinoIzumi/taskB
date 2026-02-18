using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("61ac4ecd-adf2-425b-a48f-a2c02a8b5abe"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("b0a1b050-7de8-42b0-9fcd-ae0375c0d996"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("c4fa81cc-ceef-402f-978b-c50e35e09b82"));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "TodoItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("210eec23-535a-403a-b400-316651db0402"), "#007AFF", "Work related tasks", "Work" },
                    { new Guid("51d48e62-31d8-43f0-ab1e-6667ec9ecf73"), "#FF9500", "Personal tasks", "Personal" }
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "CategoryId", "Completed", "CreatedAt", "Description", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("54676881-12fe-467f-87cc-0494e3ade227"), new Guid("210eec23-535a-403a-b400-316651db0402"), false, new DateTime(2026, 2, 18, 7, 58, 18, 814, DateTimeKind.Utc).AddTicks(3070), null, "Learn EF Core", null },
                    { new Guid("69b31b6b-669f-45c4-a733-45081743f913"), new Guid("51d48e62-31d8-43f0-ab1e-6667ec9ecf73"), false, new DateTime(2026, 2, 18, 7, 58, 18, 814, DateTimeKind.Utc).AddTicks(3070), null, "Build a Web API", null },
                    { new Guid("aceafa4b-6fcc-4f42-acc1-629731e9bd19"), new Guid("210eec23-535a-403a-b400-316651db0402"), true, new DateTime(2026, 2, 18, 7, 58, 18, 814, DateTimeKind.Utc).AddTicks(3060), null, "Learn .NET 8", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_CategoryId",
                table: "TodoItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Categories_CategoryId",
                table: "TodoItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Categories_CategoryId",
                table: "TodoItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_CategoryId",
                table: "TodoItems");

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("54676881-12fe-467f-87cc-0494e3ade227"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("69b31b6b-669f-45c4-a733-45081743f913"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("aceafa4b-6fcc-4f42-acc1-629731e9bd19"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TodoItems");

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
    }
}
