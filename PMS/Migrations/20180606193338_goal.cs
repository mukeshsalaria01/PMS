using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.Web.UI.Migrations
{
    public partial class goal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MeasureOfPerformance = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Weightage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");
        }
    }
}
