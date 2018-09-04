using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSide2.DAO.Migrations
{
    public partial class SystemUserAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM dbo.Persons WHERE UserName = \'system\'))" +
                " BEGIN" +
                " INSERT INTO dbo.Persons(UserName, Email, Created, Role)" +
                " VALUES(\'system\', \'system@admin.com\',SYSUTCDATETIME(), 1)" +
                " END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
