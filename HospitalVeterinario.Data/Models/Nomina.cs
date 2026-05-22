using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalVeterinario.Data.Models
{
    public class Nomina
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        public Empleado? Empleado { get; set; }

        [Required]
        public string TipoPago { get; set; } = string.Empty;

        [Required]
        public DateTime FechaPago { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioBase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioBruto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DescuentoIGSS { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DescuentoISR { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDescuentos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioNeto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal HorasExtras { get; set; }

        // Asistencia

        [Column(TypeName = "decimal(18,2)")]

        public decimal MontoHorasExtras { get; set; }
        public int DiasHabilesMes { get; set; }

        public int DiasTrabajados { get; set; }


        public int DiasAusentes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DescuentoAusencias { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DescuentoSeptimo { get; set; }

        // Productividad
        [Column(TypeName = "decimal(18,2)")]

        public int Mes { get; set; }

        public int Anio { get; set; }
        public decimal PorcentajeProductividad { get; set; }

        public string NivelProductividad { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Bono14 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Aguinaldo { get; set; }
    }
}