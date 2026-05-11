using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalVeterinario.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRolAUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Empleados_EmpleadoId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Empleados_EmpleadoId",
                table: "Users",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Empleados_EmpleadoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Empleados_EmpleadoId",
                table: "Users",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
