using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetNathanaelOscar.Data.Migrations
{
    public partial class AjoutTypeVetements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Vetement",
                newName: "TypeVetement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeVetement",
                table: "Vetement",
                newName: "Type");
        }
    }
}
