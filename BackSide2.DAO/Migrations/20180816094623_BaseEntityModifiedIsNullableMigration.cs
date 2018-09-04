using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSide2.DAO.Migrations
{
    public partial class BaseEntityModifiedIsNullableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(
            //    "IF (NOT EXISTS(SELECT * FROM dbo.Persons WHERE UserName = \'system\'))" +
            //    " BEGIN" +
            //    " INSERT INTO dbo.Persons(UserName, Email, Created, Role)" +
            //    " VALUES(\'system\', \'system@admin.com\',SYSUTCDATETIME(), 1)" +
            //    " END");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}