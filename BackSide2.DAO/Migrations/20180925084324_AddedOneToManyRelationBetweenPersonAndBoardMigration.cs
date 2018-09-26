using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSide2.DAO.Migrations
{
    public partial class AddedOneToManyRelationBetweenPersonAndBoardMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Boards");

            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "Boards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_PersonId",
                table: "Boards",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Persons_PersonId",
                table: "Boards",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Persons_PersonId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_PersonId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Boards");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Boards",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
