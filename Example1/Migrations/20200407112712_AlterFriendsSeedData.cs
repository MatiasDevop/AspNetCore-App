using Microsoft.EntityFrameworkCore.Migrations;

namespace Example1.Migrations
{
    public partial class AlterFriendsSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "mail@mail.com");

            migrationBuilder.InsertData(
                table: "Friends",
                columns: new[] { "Id", "City", "Email", "Name" },
                values: new object[] { 3, 8, "lllmail@mail3.com", "Laura" });

            migrationBuilder.InsertData(
                table: "Friends",
                columns: new[] { "Id", "City", "Email", "Name" },
                values: new object[] { 2, 2, "pejjp@mail.com", "Juan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "pep@mail.com");
        }
    }
}
