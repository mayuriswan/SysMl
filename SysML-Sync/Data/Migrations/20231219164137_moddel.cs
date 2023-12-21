using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysML_Sync.Data.Migrations
{
    public partial class moddel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moddels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UmlContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotationContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modeltype = table.Column<int>(type: "int", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moddels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moddels");
        }
    }
}
