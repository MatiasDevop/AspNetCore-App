using Microsoft.EntityFrameworkCore.Migrations;

namespace Example1.Migrations
{
    public partial class AddRutaPhotoFriend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "routePhoto",
                table: "Friends",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "routePhoto",
                table: "Friends");
        }
    }
}
