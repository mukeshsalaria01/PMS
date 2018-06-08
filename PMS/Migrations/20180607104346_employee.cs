using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PMS.Migrations
{
    public partial class employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppraisalPeriod = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    CreatedBy = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Department = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    FirstName = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    Grade = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    Number = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
