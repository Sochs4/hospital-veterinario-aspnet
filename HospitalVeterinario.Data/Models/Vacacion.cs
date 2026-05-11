using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalVeterinario.Data.Models
{
    public class Vacacion
    {
        [Key]
        public int Id { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int DiasSolicitados { get; set; }

        public string Motivo { get; set; } = string.Empty;

        public string Estado { get; set; } = "Pendiente";
        // Pendiente, Aprobada, Rechazada

        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
    }
}