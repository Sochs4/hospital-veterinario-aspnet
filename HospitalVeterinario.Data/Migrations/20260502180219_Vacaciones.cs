using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalVeterinario.Data.Migrations
{
    /// <inheritdoc />
    public partial class Vacaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalioDeVacaciones",
                table: "Vacacion");

            migrationBuilder.AddColumn<int>(
                name: "DiasSolicitados",
                table: "Vacacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Vacacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitud",
                table: "Vacacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Vacacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasSolicitados",
                table: "Vacacion");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Vacacion");

            migrationBuilder.DropColumn(
                name: "FechaSolicitud",
                table: "Vacacion");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Vacacion");

            migrationBuilder.AddColumn<bool>(
                name: "SalioDeVacaciones",
                table: "Vacacion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
