using HospitalVeterinario.Data.Models;
using HospitalVeterinario.Data.Repositories;
using System.Collections.Generic;

namespace HospitalVeterinario.Services
{
    public interface IDepartamentoService
    {
        IEnumerable<Departamento> GetAllDepartamentos();
        Departamento? GetDepartamentoById(int id);
        void AddDepartamento(Departamento departamento);
        void UpdateDepartamento(Departamento departamento);
        void DeleteDepartamento(int id);
    }

    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public IEnumerable<Departamento> GetAllDepartamentos()
        {
            return _departamentoRepository.GetAll();
        }

        public Departamento? GetDepartamentoById(int id)
        {
            return _departamentoRepository.GetById(id);
        }

        public void AddDepartamento(Departamento departamento)
        {
            _departamentoRepository.Add(departamento);
        }

        public void UpdateDepartamento(Departamento departamento)
        {
            _departamentoRepository.Update(departamento);
        }

        public void DeleteDepartamento(int id)
        {
            _departamentoRepository.Delete(id);
        }
    }
}
