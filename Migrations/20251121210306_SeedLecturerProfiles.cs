using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ST10446806_PROG6212_POEPART1.Migrations
{
    /// <inheritdoc />
    public partial class SeedLecturerProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "LecturerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "LecturerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "LecturerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LecturerProfiles",
                keyColumn: "LecturerID",
                keyValue: 1,
                columns: new[] { "Email", "FullName", "HourlyRate", "PhoneNumber" },
                values: new object[] { "doe.eye@gmail.com", "Doe Eye", 0m, "1234567890" });

            migrationBuilder.UpdateData(
                table: "LecturerProfiles",
                keyColumn: "LecturerID",
                keyValue: 2,
                columns: new[] { "Email", "FullName", "HourlyRate", "PhoneNumber", "UserID" },
                values: new object[] { "john.smith@gmail.com", "John Smith", 0m, "0987654321", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "FullName",
                value: "Doe Eye");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "FullName",
                value: "John Smith");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "FullName",
                value: "Michael Jackson");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "FullName",
                value: "Miss Piggy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "FullName",
                value: "Linken Black");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "FullName", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 7, "Emma HR", "1234", 3, "hr1" },
                    { 8, "Kermit Frog", "1234", 3, "hr2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "LecturerProfiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "LecturerProfiles");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "LecturerProfiles");

            migrationBuilder.UpdateData(
                table: "LecturerProfiles",
                keyColumn: "LecturerID",
                keyValue: 1,
                column: "HourlyRate",
                value: 300m);

            migrationBuilder.UpdateData(
                table: "LecturerProfiles",
                keyColumn: "LecturerID",
                keyValue: 2,
                columns: new[] { "HourlyRate", "UserID" },
                values: new object[] { 350m, 4 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "FullName",
                value: "John Doe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "FullName",
                value: "Sarah Smith");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "FullName",
                value: "Michael Brown");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "FullName",
                value: "Bob Green");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "FullName",
                value: "Carol Black");
        }
    }
}
