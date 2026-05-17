using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaSoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoActivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiereReceta",
                table: "Medicamentos");

            migrationBuilder.RenameColumn(
                name: "PrincipioActivo",
                table: "Medicamentos",
                newName: "Laboratorio");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Medicamentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Medicamentos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Medicamentos");

            migrationBuilder.RenameColumn(
                name: "Laboratorio",
                table: "Medicamentos",
                newName: "PrincipioActivo");

            migrationBuilder.AddColumn<bool>(
                name: "RequiereReceta",
                table: "Medicamentos",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }
    }
}
