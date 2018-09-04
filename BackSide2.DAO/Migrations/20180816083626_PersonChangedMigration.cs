using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSide2.DAO.Migrations
{
    public partial class PersonChangedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "WhoModifiedLast",
                table: "Persons");

            migrationBuilder.AlterColumn<long>(
                name: "Language",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Persons",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Modified",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Persons");

            migrationBuilder.AlterColumn<long>(
                name: "Language",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Creator",
                table: "Persons",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WhoModifiedLast",
                table: "Persons",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
