using Microsoft.EntityFrameworkCore.Migrations;

namespace TODOLIST.Data.Migrations
{
    public partial class AddItemUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Items");
        }
    }
}
