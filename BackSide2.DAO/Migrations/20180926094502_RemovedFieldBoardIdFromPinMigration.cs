using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSide2.DAO.Migrations
{
    public partial class RemovedFieldBoardIdFromPinMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Pins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BoardId",
                table: "Pins",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
