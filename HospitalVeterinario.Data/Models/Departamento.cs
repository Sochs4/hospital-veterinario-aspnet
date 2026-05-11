// Archivo: HospitalVeterinario.Data/Models/Departamento.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalVeterinario.Data.Models // Ajusta si es necesario
{
    public class Departamento
    {
        [Key]
        public int IdDepartamento { get; set; }

        [StringLength(100)]
        public string? NombreDepartamento { get; set; }

        [StringLength(255)]
        public string? Descripcion { get; set; }

        // public virtual ICollection<Empleado>? Empleados { get; set; }
    }
}