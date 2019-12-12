using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Data.Migrations
{
    public partial class userID_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contacts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contacts");
        }
    }
}
