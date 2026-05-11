using HospitalVeterinario.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace HospitalVeterinario.Data.Repositories
{
    public interface IEmpleadoRepository
    {
        IEnumerable<Empleado> GetAll();
        Empleado? GetById(int id); // Tipo de retorno anulable
        void Add(Empleado empleado);
        void Update(Empleado empleado);
        void Delete(int id);
    }

    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=RecursosHumanosDB3;Trusted_Connection=True;TrustServerCertificate=True";

        public IEnumerable<Empleado> GetAll()
        {
            var empleados = new List<Empleado>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Empleados", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var empleado = new Empleado
                        {
                            IdEmpleado = (int)reader["IdEmpleado"],
                            Nombre = reader["Nombre"] == DBNull.Value ? null : reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"] == DBNull.Value ? null : reader["Apellido"].ToString(),
                            FechaNacimiento = reader["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["FechaNacimiento"],
                            Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                            Telefono = reader["Telefono"] == DBNull.Value ? null : reader["Telefono"].ToString(),
                            FechaContratacion = reader["FechaContratacion"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["FechaContratacion"],
                            DPI = reader["DPI"] == DBNull.Value ? null : reader["DPI"].ToString(),
                            NIT = reader["NIT"] == DBNull.Value ? null : reader["NIT"].ToString(),
                            Genero = reader["Genero"] == DBNull.Value ? null : reader["Genero"].ToString(),
                            IdDepartamento = reader["IdDepartamento"] == DBNull.Value ? (int?)null : (int)reader["IdDepartamento"],
                            IdPuesto = reader["IdPuesto"] == DBNull.Value ? (int?)null : (int)reader["IdPuesto"],

                            // 🔥 ESTO FALTABA
                            SalarioBase = reader["SalarioBase"] == DBNull.Value ? (decimal?)null : (decimal)reader["SalarioBase"],
                            BonificacionDecreto = reader["BonificacionDecreto"] == DBNull.Value ? (decimal?)null : (decimal)reader["BonificacionDecreto"],
                            EstadoEmpleado = reader["EstadoEmpleado"] == DBNull.Value ? null : reader["EstadoEmpleado"].ToString()
                        };
                        empleados.Add(empleado);
                    }
                }
            }
            return empleados;
        }
        public Empleado? GetById(int id)
        {
            Empleado? empleado = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Empleados WHERE IdEmpleado = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empleado = new Empleado
                            {
                                IdEmpleado = (int)reader["IdEmpleado"],
                                Nombre = reader["Nombre"] == DBNull.Value ? null : reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"] == DBNull.Value ? null : reader["Apellido"].ToString(),
                                FechaNacimiento = reader["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["FechaNacimiento"],
                                Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                                Telefono = reader["Telefono"] == DBNull.Value ? null : reader["Telefono"].ToString(),
                                FechaContratacion = reader["FechaContratacion"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["FechaContratacion"],
                                DPI = reader["DPI"] == DBNull.Value ? null : reader["DPI"].ToString(),
                                NIT = reader["NIT"] == DBNull.Value ? null : reader["NIT"].ToString(),
                                Genero = reader["Genero"] == DBNull.Value ? null : reader["Genero"].ToString(),
                                IdDepartamento = reader["IdDepartamento"] == DBNull.Value ? (int?)null : (int)reader["IdDepartamento"],
                                IdPuesto = reader["IdPuesto"] == DBNull.Value ? (int?)null : (int)reader["IdPuesto"],

                                SalarioBase = reader["SalarioBase"] == DBNull.Value ? (decimal?)null : (decimal)reader["SalarioBase"],
                                BonificacionDecreto = reader["BonificacionDecreto"] == DBNull.Value ? (decimal?)null : (decimal)reader["BonificacionDecreto"],
                                NumeroIGSS = reader["NumeroIGSS"] == DBNull.Value ? null : reader["NumeroIGSS"].ToString(),
                                EstadoEmpleado = reader["EstadoEmpleado"] == DBNull.Value ? null : reader["EstadoEmpleado"].ToString(),
                                NombreBanco = reader["NombreBanco"] == DBNull.Value ? null : reader["NombreBanco"].ToString(),
                                NumeroCuentaBancaria = reader["NumeroCuentaBancaria"] == DBNull.Value ? null : reader["NumeroCuentaBancaria"].ToString(),
                                TipoCuentaBancaria = reader["TipoCuentaBancaria"] == DBNull.Value ? null : reader["TipoCuentaBancaria"].ToString()
                            };
                        }
                    }
                }
            }

            return empleado;
        }
        

        public void Add(Empleado empleado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(@"
           INSERT INTO Empleados
(Nombre, Apellido, FechaNacimiento, Email, Telefono, FechaContratacion,
 DPI, NIT, Genero, IdDepartamento, IdPuesto, SalarioBase, BonificacionDecreto,
 NumeroIGSS, EstadoEmpleado, NombreBanco, NumeroCuentaBancaria, TipoCuentaBancaria, HasUser)

VALUES
(@Nombre, @Apellido, @FechaNacimiento, @Email, @Telefono, @FechaContratacion,
 @DPI, @NIT, @Genero, @IdDepartamento, @IdPuesto, @SalarioBase, @BonificacionDecreto,
 @NumeroIGSS, @EstadoEmpleado, @NombreBanco, @NumeroCuentaBancaria, @TipoCuentaBancaria, @HasUser);
        ", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", empleado.Nombre ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Apellido", empleado.Apellido ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", empleado.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefono", empleado.Telefono ?? (object)DBNull.Value);

                    // 🔥 ESTE ERA EL ERROR
                    command.Parameters.AddWithValue("@FechaContratacion",
                    empleado.FechaContratacion ?? DateTime.Now);

                    command.Parameters.AddWithValue("@DPI", empleado.DPI ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NIT", empleado.NIT ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Genero", empleado.Genero ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdDepartamento", empleado.IdDepartamento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdPuesto", empleado.IdPuesto ?? (object)DBNull.Value);

                    command.Parameters.AddWithValue("@SalarioBase", empleado.SalarioBase ?? (object)DBNull.Value);

                    // 🔥 BONIFICACIÓN FIJA
                    command.Parameters.AddWithValue("@BonificacionDecreto", 250);

                    command.Parameters.AddWithValue("@NumeroIGSS", empleado.NumeroIGSS ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@EstadoEmpleado", empleado.EstadoEmpleado ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NombreBanco", empleado.NombreBanco ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NumeroCuentaBancaria", empleado.NumeroCuentaBancaria ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TipoCuentaBancaria", empleado.TipoCuentaBancaria ?? (object)DBNull.Value);

                    // Al crear empleado todavía no tiene usuario
                    command.Parameters.AddWithValue("@HasUser", false);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Empleado empleado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"UPDATE Empleados SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Email = @Email, Telefono = @Telefono,
                                                    FechaContratacion = @FechaContratacion, DPI = @DPI, NIT = @NIT, Genero = @Genero, IdDepartamento = @IdDepartamento, IdPuesto = @IdPuesto
                                                    WHERE IdEmpleado = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@Id", empleado.IdEmpleado);
                    command.Parameters.AddWithValue("@Nombre", empleado.Nombre ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Apellido", empleado.Apellido ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", empleado.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefono", empleado.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FechaContratacion", empleado.FechaContratacion);
                    command.Parameters.AddWithValue("@DPI", empleado.DPI ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NIT", empleado.NIT ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Genero", empleado.Genero ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdDepartamento", empleado.IdDepartamento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdPuesto", empleado.IdPuesto ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Empleados WHERE IdEmpleado = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}