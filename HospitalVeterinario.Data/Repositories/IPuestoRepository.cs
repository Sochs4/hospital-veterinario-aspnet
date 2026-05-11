using HospitalVeterinario.Data.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace HospitalVeterinario.Data.Repositories
{
    public interface IPuestoRepository
    {
        IEnumerable<Puesto> GetAll();
        Puesto? GetById(int id);
        void Add(Puesto puesto);
        void Update(Puesto puesto);
        void Delete(int id);
    }

    public class PuestoRepository : IPuestoRepository
    {
        private readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=RecursosHumanosDB3;Trusted_Connection=True;TrustServerCertificate=True;";
        public IEnumerable<Puesto> GetAll()
        {
            var puestos = new List<Puesto>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Puestos", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       var puesto = new Puesto
                        {
                            IdPuesto = (int)reader["IdPuesto"],
                            NombrePuesto = reader["NombrePuesto"] == DBNull.Value ? null : reader["NombrePuesto"].ToString(),
                            DescripcionPuesto = reader["DescripcionPuesto"] == DBNull.Value ? null : reader["DescripcionPuesto"].ToString(),
                            NivelJerarquico = reader["NivelJerarquico"] == DBNull.Value ? null : reader["NivelJerarquico"].ToString(),
                            SalarioBase = reader["SalarioBase"] == DBNull.Value ? (decimal?)null : (decimal)reader["SalarioBase"],
                            Especialidad = reader["Especialidad"] == DBNull.Value ? null : reader["Especialidad"].ToString(),
                            IdDepartamento = reader["IdDepartamento"] == DBNull.Value ? (int?)null : (int)reader["IdDepartamento"]
                        };
                        puestos.Add(puesto);
                    }
                }
            }
            return puestos;
        }

        public Puesto? GetById(int id)
        {
            Puesto? puesto = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Puestos WHERE IdPuesto = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            puesto = new Puesto
                            {
                                IdPuesto = (int)reader["IdPuesto"],
                                NombrePuesto = reader["NombrePuesto"] == DBNull.Value ? null : reader["NombrePuesto"].ToString(),
                                DescripcionPuesto = reader["DescripcionPuesto"] == DBNull.Value ? null : reader["DescripcionPuesto"].ToString(),
                                NivelJerarquico = reader["NivelJerarquico"] == DBNull.Value ? null : reader["NivelJerarquico"].ToString(),
                                SalarioBase = reader["SalarioBase"] == DBNull.Value ? (decimal?)null : (decimal)reader["SalarioBase"],
                                Especialidad = reader["Especialidad"] == DBNull.Value ? null : reader["Especialidad"].ToString()
                            };
                        }
                    }
                }
            }
            return puesto;
        }

        public void Add(Puesto puesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"INSERT INTO Puestos (NombrePuesto, DescripcionPuesto, NivelJerarquico, SalarioBase, Especialidad)
                                                    VALUES (@Nombre, @Descripcion, @Nivel, @Salario, @Especialidad);", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", puesto.NombrePuesto ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Descripcion", puesto.DescripcionPuesto ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Nivel", puesto.NivelJerarquico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Salario", puesto.SalarioBase ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Especialidad", puesto.Especialidad ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Puesto puesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"UPDATE Puestos SET NombrePuesto = @Nombre, DescripcionPuesto = @Descripcion, NivelJerarquico = @Nivel, SalarioBase = @Salario, Especialidad = @Especialidad
                                                    WHERE IdPuesto = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@Id", puesto.IdPuesto);
                    command.Parameters.AddWithValue("@Nombre", puesto.NombrePuesto ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Descripcion", puesto.DescripcionPuesto ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Nivel", puesto.NivelJerarquico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Salario", puesto.SalarioBase ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Especialidad", puesto.Especialidad ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Puestos WHERE IdPuesto = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}