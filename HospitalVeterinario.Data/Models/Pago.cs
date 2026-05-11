using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalVeterinario.Data.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MontoBruto { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DescuentoIGSS { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DescuentoISR { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MontoNeto { get; set; }

        [StringLength(200)]
        public string? Descripcion { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado Empleado { get; set; } = null!;

        // 🆕 NUEVOS CAMPOS PARA PAGOS MENSUALES
        public int? MesPagado { get; set; }        // 1 = Enero, 2 = Febrero, etc.
        public int? AnioPagado { get; set; }       // Año del pago
        public int? DiasLaborados { get; set; }    // Días laborados excluyendo descansos
        public int? HorasExtras { get; set; }      // Cantidad de horas extras

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PagoHorasExtras { get; set; } // Pago total por horas extras

        public string? Quincena { get; set; } // para técnicos veterinarios
        public decimal? BonificacionDecreto { get; set; }

        public string?  TipoPago { get; set; } // "mes", "quincena1", "semana2", etc.

        public decimal? BonoEspecial { get; set; }


        public bool EsIndemnizacion { get; set; } = false;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Indemnizacion
        {
            get; set;




        }

    }

}
