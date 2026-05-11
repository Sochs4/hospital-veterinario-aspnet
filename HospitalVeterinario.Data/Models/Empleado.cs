// Archivo: HospitalVeterinario.Data/Models/Empleado.cs

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalVeterinario.Data.Models
{
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }

        [StringLength(100)]
        public string? Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        public DateTime? FechaContratacion { get; set; }

        [StringLength(15)]
        public string? DPI { get; set; }

        [StringLength(20)]
        public string? NIT { get; set; }

        [StringLength(20)]
        public string? Genero { get; set; }

        public int? IdDepartamento { get; set; }
        public int? IdPuesto { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? SalarioBase { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? BonificacionDecreto { get; set; }

        [StringLength(20)]
        public string? NumeroIGSS { get; set; }

        [StringLength(50)]
        public string? TipoContrato { get; set; }

        [StringLength(20)]
        public string? EstadoEmpleado { get; set; }

        [StringLength(100)]
        public string? NombreBanco { get; set; }

        [StringLength(30)]
        public string? NumeroCuentaBancaria { get; set; }

        [StringLength(20)]
        public string? TipoCuentaBancaria { get; set; }

        // Nueva propiedad
        public bool HasUser { get; set; } = false;

        // Relación con tabla Puesto
        [ForeignKey("IdPuesto")]
        public Puesto? IdPuestoNavigation { get; set; }

        public bool Activo { get; set; } = true;
        // Relación con asistencias
        public ICollection<Asistencia>? Asistencias { get; set; }
    }
}
