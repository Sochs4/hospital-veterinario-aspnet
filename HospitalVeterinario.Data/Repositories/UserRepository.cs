using HospitalVeterinario.Data.Models;
using Microsoft.Data.SqlClient;

namespace HospitalVeterinario.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=RecursosHumanosDB3;Trusted_Connection=True;TrustServerCertificate=True";

        public User? GetByEmpleadoId(int empleadoId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Users WHERE EmpleadoId = @Id", connection);
            command.Parameters.AddWithValue("@Id", empleadoId);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    Username = reader["Username"] == DBNull.Value ? string.Empty : reader["Username"].ToString()!,
                    PasswordHash = reader["PasswordHash"] == DBNull.Value ? string.Empty : reader["PasswordHash"].ToString()!,
                    EmpleadoId = (int)reader["EmpleadoId"]
                };
            }

            return null;
        }

        public void Add(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(@"
                INSERT INTO Users (Username, PasswordHash, EmpleadoId)
                VALUES (@Username, @PasswordHash, @EmpleadoId)", connection);

            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@EmpleadoId", user.EmpleadoId);

            command.ExecuteNonQuery();
        }
    }
}