
using HospitalVeterinario.Data.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace HospitalVeterinario.Data.Repositories
{
    public interface IDepartamentoRepository
    {
        IEnumerable<Departamento> GetAll();
        Departamento? GetById(int id);
        void Add(Departamento departamento);
        void Update(Departamento departamento);
        void Delete(int id);
    }

    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=RecursosHumanosDB3;Trusted_Connection=True;TrustServerCertificate=True";

        public IEnumerable<Departamento> GetAll()
        {
            var departamentos = new List<Departamento>();
            using (var connection = new SqlConnection(_connectionString))
            {       
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Departamentos", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var departamento = new Departamento
                        {
                            IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                            NombreDepartamento = reader["NombreDepartamento"].ToString(),
                            Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString()
                        };
                        departamentos.Add(departamento);
                    }
                }   
            }
            return departamentos;
        }


        public Departamento? GetById(int id)
        {
            Departamento? departamento = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Departamentos WHERE IdDepartamento = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            departamento = new Departamento
                            {
                                IdDepartamento = (int)reader["IdDepartamento"],
                                NombreDepartamento = reader["NombreDepartamento"] == DBNull.Value ? null : reader["NombreDepartamento"].ToString(),
                                Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString()
                            };
                        }
                    }
                }
            }
            return departamento;
        }

        public void Add(Departamento departamento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Departamentos (NombreDepartamento, Descripcion) VALUES (@Nombre, @Descripcion);", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", departamento.NombreDepartamento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Descripcion", departamento.Descripcion ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Departamento departamento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Departamentos SET NombreDepartamento = @Nombre, Descripcion = @Descripcion WHERE IdDepartamento = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@Id", departamento.IdDepartamento);
                    command.Parameters.AddWithValue("@Nombre", departamento.NombreDepartamento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Descripcion", departamento.Descripcion ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Departamentos WHERE IdDepartamento = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}