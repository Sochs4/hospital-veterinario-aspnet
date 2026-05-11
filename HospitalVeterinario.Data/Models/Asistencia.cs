using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalVeterinario.Data.Models
{
    public class Asistencia
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }

        public DateTime Fecha { get; set; }

        public bool Asistio { get; set; }

        public Empleado Empleado { get; set; }
    }
}
