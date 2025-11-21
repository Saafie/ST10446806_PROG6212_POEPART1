using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ST10446806_PROG6212_POEPART1.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndLecturers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalHours",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.InsertData(
                table: "LecturerProfiles",
                columns: new[] { "LecturerID", "BankDetails", "HourlyRate", "UserID" },
                values: new object[,]
                {
                    { 1, "Bank A", 300m, 1 },
                    { 2, "Bank B", 350m, 4 }
                });

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "Alice White", 0, "lecturer2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "Bob Green", 1, "coordinator2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "FullName",
                value: "Carol Black");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LecturerProfiles",
                keyColumn: "LecturerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LecturerProfiles",
                keyColumn: "LecturerID",
                keyValue: 2);

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "David Johnson", 1, "coordinator2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "FullName", "Role", "Username" },
                values: new object[] { "Michael Brown", 2, "manager1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "FullName",
                value: "Emily Davis");
        }
    }
}
