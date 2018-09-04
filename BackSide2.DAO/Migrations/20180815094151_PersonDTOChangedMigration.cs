using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSide2.DAO.Migrations
{
    public partial class PersonDTOChangedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.DropPrimaryKey("PK_Persons", "Persons");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Persons",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddPrimaryKey("PK_Persons", "Persons", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.DropPrimaryKey("PK_Persons", "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey("PK_Persons", "Persons", "Id");
        }
    }
}
