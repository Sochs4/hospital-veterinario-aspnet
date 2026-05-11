using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalVeterinario.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMesAnioNomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Anio",
                table: "Nominas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Mes",
                table: "Nominas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anio",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "Nominas");
        }
    }
}
