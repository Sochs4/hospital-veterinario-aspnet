using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalVeterinario.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarAsistenciaProductividadNomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DescuentoAusencias",
                table: "Nominas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescuentoSeptimo",
                table: "Nominas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DiasAusentes",
                table: "Nominas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiasHabilesMes",
                table: "Nominas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiasTrabajados",
                table: "Nominas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NivelProductividad",
                table: "Nominas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeProductividad",
                table: "Nominas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescuentoAusencias",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "DescuentoSeptimo",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "DiasAusentes",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "DiasHabilesMes",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "DiasTrabajados",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "NivelProductividad",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "PorcentajeProductividad",
                table: "Nominas");
        }
    }
}
