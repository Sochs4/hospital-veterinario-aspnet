using HospitalVeterinario.Data.Models;
using HospitalVeterinario.Data.Repositories;

namespace HospitalVeterinario.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetByEmpleadoId(int empleadoId)
        {
            return _userRepository.GetByEmpleadoId(empleadoId);
        }

        public void Add(User user)
        {
            _userRepository.Add(user);
        }
    }
}