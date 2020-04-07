using Microsoft.EntityFrameworkCore.Migrations;

namespace Example1.Migrations
{
    public partial class SeedFriendsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Friends",
                columns: new[] { "Id", "City", "Email", "Name" },
                values: new object[] { 1, 4, "pep@mail.com", "PEPE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
