using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetNathanaelOscar.Data.Migrations
{
    public partial class CreationNouvelleDonneeVetement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cote",
                table: "Vetement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DateObtention",
                table: "Vetement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Vetement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Vetement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Vetement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cote",
                table: "Vetement");

            migrationBuilder.DropColumn(
                name: "DateObtention",
                table: "Vetement");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Vetement");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vetement");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Vetement");
        }
    }
}
