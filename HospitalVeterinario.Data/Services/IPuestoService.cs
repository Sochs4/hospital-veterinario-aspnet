using HospitalVeterinario.Data.Models;
using HospitalVeterinario.Data.Repositories;
using System.Collections.Generic;

namespace HospitalVeterinario.Services
{
    public interface IPuestoService
    {
        IEnumerable<Puesto> GetAllPuestos();
        Puesto? GetPuestoById(int id);
        void AddPuesto(Puesto puesto);
        void UpdatePuesto(Puesto puesto);
        void DeletePuesto(int id);
    }
    public class PuestoService : IPuestoService
    {
        private readonly IPuestoRepository _puestoRepository;

        public PuestoService(IPuestoRepository puestoRepository)
        {
            _puestoRepository = puestoRepository;
        }

        public IEnumerable<Puesto> GetAllPuestos()
        {
            return _puestoRepository.GetAll();
        }

        public Puesto? GetPuestoById(int id)
        {
            return _puestoRepository.GetById(id);
        }

        public void AddPuesto(Puesto puesto)
        {
            _puestoRepository.Add(puesto);
        }

        public void UpdatePuesto(Puesto puesto)
        {
            _puestoRepository.Update(puesto);
        }

        public void DeletePuesto(int id)
        {
            _puestoRepository.Delete(id);
        }
    }
}
