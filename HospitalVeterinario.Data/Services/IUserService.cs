using HospitalVeterinario.Data.Models;

namespace HospitalVeterinario.Services
{
    public interface IUserService
    {
        User? GetByEmpleadoId(int empleadoId);
        void Add(User user);
    }
}