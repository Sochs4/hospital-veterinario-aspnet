// Archivo: HospitalVeterinario.Data/Models/Puesto.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalVeterinario.Data.Models // Ajusta si es necesario
{
    public class Puesto
    {
        [Key]
        public int IdPuesto { get; set; }

        [StringLength(100)]
        public string? NombrePuesto { get; set; }

        [StringLength(255)]
        public string? DescripcionPuesto { get; set; }

        [StringLength(50)]
        public string? NivelJerarquico { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SalarioBase { get; set; }

        [StringLength(100)]
        public string? Especialidad { get; set; }

        public int? IdDepartamento { get; set; }  // 👈 ESTA ES LA CLAVE
    }
}