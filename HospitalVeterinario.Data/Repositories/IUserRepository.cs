using HospitalVeterinario.Data.Models;

namespace HospitalVeterinario.Data.Repositories
{
    public interface IUserRepository
    {
        User? GetByEmpleadoId(int empleadoId);
        void Add(User user);
    }
}