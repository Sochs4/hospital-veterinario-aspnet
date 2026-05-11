using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalVeterinario.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    IdDepartamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.IdDepartamento);
                });

            migrationBuilder.CreateTable(
                name: "Puestos",
                columns: table => new
                {
                    IdPuesto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePuesto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescripcionPuesto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NivelJerarquico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Especialidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdDepartamento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puestos", x => x.IdPuesto);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FechaContratacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DPI = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    NIT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdDepartamento = table.Column<int>(type: "int", nullable: true),
                    IdPuesto = table.Column<int>(type: "int", nullable: true),
                    SalarioBase = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    BonificacionDecreto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    NumeroIGSS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TipoContrato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstadoEmpleado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NombreBanco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroCuentaBancaria = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TipoCuentaBancaria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HasUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Puestos_IdPuesto",
                        column: x => x.IdPuesto,
                        principalTable: "Puestos",
                        principalColumn: "IdPuesto");
                });

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Asistio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asistencias_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nominas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    TipoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalarioBruto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoIGSS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoISR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDescuentos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalarioNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HorasExtras = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nominas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoBruto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DescuentoIGSS = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DescuentoISR = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MontoNeto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MesPagado = table.Column<int>(type: "int", nullable: true),
                    AnioPagado = table.Column<int>(type: "int", nullable: true),
                    DiasLaborados = table.Column<int>(type: "int", nullable: true),
                    HorasExtras = table.Column<int>(type: "int", nullable: true),
                    PagoHorasExtras = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Quincena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BonificacionDecreto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TipoPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BonoEspecial = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EsIndemnizacion = table.Column<bool>(type: "bit", nullable: false),
                    Indemnizacion = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalioDeVacaciones = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacacion_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_EmpleadoId",
                table: "Asistencias",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdPuesto",
                table: "Empleados",
                column: "IdPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_EmpleadoId",
                table: "Nominas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_EmpleadoId",
                table: "Pagos",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmpleadoId",
                table: "Users",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacacion_EmpleadoId",
                table: "Vacacion",
                column: "EmpleadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Nominas");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vacacion");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Puestos");
        }
    }
}
