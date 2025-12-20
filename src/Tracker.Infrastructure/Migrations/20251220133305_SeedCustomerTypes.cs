using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomerTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "customer_types",
                columns: new[] { "customer_type_id", "description" },
                values: new object[,]
                {
                    { 1, "Individual" },
                    { 2, "Company" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer_types",
                keyColumn: "customer_type_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "customer_types",
                keyColumn: "customer_type_id",
                keyValue: 2);
        }
    }
}
