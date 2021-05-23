using Microsoft.EntityFrameworkCore.Migrations;

namespace Dinolab.Migrations
{
    public partial class LabList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabList",
                columns: table => new
                {
                    LabId = table.Column<string>(type: "TEXT", nullable: false),
                    LabName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabList", x => x.LabId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabList");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
