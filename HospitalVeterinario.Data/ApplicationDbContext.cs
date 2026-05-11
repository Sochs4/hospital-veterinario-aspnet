// Archivo: HospitalVeterinario.Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using HospitalVeterinario.Data.Models;
// Asegúrate de tener esta using si User.cs está en el proyecto Web

namespace HospitalVeterinario.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Nomina> Nominas { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }


        public DbSet<Vacacion> Vacacion { get; set; }

        // Reemplazamos CrearUsuarioViewModel con User

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- TODO EL CONTENIDO DE OnModelCreating ESTÁ COMENTADO PARA ESTA PRUEBA ---
            // La idea es depender de los atributos [Key] y [Column(TypeName="...")]
            // directamente en tus clases de modelo (Empleado.cs, Departamento.cs, Puesto.cs)

            // modelBuilder.Entity<Empleado>(entity =>
            // {
            //    // entity.HasKey(e => e.IdEmpleado);
            //    // entity.Property(e => e.SalarioBase).HasColumnType("decimal(10, 2)");
            //    // entity.Property(e => e.BonificacionDecreto).HasColumnType("decimal(10, 2)");
            // });

            // modelBuilder.Entity<Departamento>(entity =>
            // {
            //    // entity.HasKey(d => d.IdDepartamento);
            // });

            // modelBuilder.Entity<Puesto>(entity =>
            // {
            //    // entity.HasKey(p => p.IdPuesto);
            //    // entity.Property(p => p.SalarioBase)
            //    //        .HasColumnType("decimal(18, 2)");
            // });
        }
    }
}