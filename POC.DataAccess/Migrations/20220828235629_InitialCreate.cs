using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExampleTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount3 = table.Column<int>(type: "int", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: false),
                    HasError = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleTables", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExampleTables");
        }
    }
}
