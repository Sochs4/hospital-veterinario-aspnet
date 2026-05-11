using HospitalVeterinario.Data.Models;
using HospitalVeterinario.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalVeterinario.Services
{
    public interface IEmpleadoService
    {
        IEnumerable<Empleado> GetAllEmpleados();
        Empleado? GetEmpleadoById(int id);
        void AddEmpleado(Empleado empleado);
        void UpdateEmpleado(Empleado empleado);
        void DeleteEmpleado(int id);
    }

    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public IEnumerable<Empleado> GetAllEmpleados()
        {
            return _empleadoRepository.GetAll();
        }

        public Empleado? GetEmpleadoById(int id)
        {
            return _empleadoRepository.GetById(id);
        }

        public void AddEmpleado(Empleado empleado)
        {
            _empleadoRepository.Add(empleado);
        }

        public void UpdateEmpleado(Empleado empleado)
        {
            _empleadoRepository.Update(empleado);
        }

        public void DeleteEmpleado(int id)
        {
            _empleadoRepository.Delete(id);
        }
    }
}