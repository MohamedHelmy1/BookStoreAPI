using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9305cbc9-4112-468a-927d-a2fea07c7153");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff63dcb3-453e-4a6e-97b9-1f04cbb3fc75");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c091ac0-e8a6-4a84-b6b3-879095b7b21d", null, "customer", "CUSTOMER" },
                    { "68a1e712-a9f9-4c1f-8f56-705a2e5dd670", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c091ac0-e8a6-4a84-b6b3-879095b7b21d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68a1e712-a9f9-4c1f-8f56-705a2e5dd670");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9305cbc9-4112-468a-927d-a2fea07c7153", null, "admin", null },
                    { "ff63dcb3-453e-4a6e-97b9-1f04cbb3fc75", null, "customer", null }
                });
        }
    }
}
