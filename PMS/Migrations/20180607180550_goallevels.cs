using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.Web.UI.Migrations
{
    public partial class goallevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Goals");

            migrationBuilder.AlterColumn<int>(
                name: "Weightage",
                table: "Goals",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "Guideline",
                table: "Goals",
                type: "VARCHAR(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level1",
                table: "Goals",
                type: "VARCHAR(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level2",
                table: "Goals",
                type: "VARCHAR(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level3",
                table: "Goals",
                type: "VARCHAR(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level4",
                table: "Goals",
                type: "VARCHAR(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level5",
                table: "Goals",
                type: "VARCHAR(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guideline",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Level1",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Level2",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Level3",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Level4",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Level5",
                table: "Goals");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weightage",
                table: "Goals",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Goals",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
