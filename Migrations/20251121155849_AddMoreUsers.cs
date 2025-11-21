using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ST10446806_PROG6212_POEPART1.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalHours",
                table: "Claims",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "Jane Smith", 0, "lecturer2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "Sarah Smith", 1, "coordinator1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "FullName", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 4, "David Johnson", "1234", 1, "coordinator2" },
                    { 5, "Michael Brown", "1234", 2, "manager1" },
                    { 6, "Emily Davis", "1234", 2, "manager2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalHours",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "Sarah Smith", 1, "coordinator1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "Michael Brown", 2, "manager1" });
        }
    }
}
