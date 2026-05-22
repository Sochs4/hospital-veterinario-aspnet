using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalVeterinario.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMontoHorasExtras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MontoHorasExtras",
                table: "Nominas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoHorasExtras",
                table: "Nominas");
        }
    }
}
