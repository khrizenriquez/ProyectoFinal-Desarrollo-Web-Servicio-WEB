using Microsoft.EntityFrameworkCore;
using SeguroMedicoAPI.Models;

namespace SeguroMedicoAPI.Data
{
    public class SeguroMedicoContext : DbContext
    {
        public SeguroMedicoContext(DbContextOptions<SeguroMedicoContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<PagoPrima> PagosPrima { get; set; }
        public DbSet<ConsultaCobertura> ConsultasCobertura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>()
                .HasKey(p => p.CodigoPaciente);

            modelBuilder.Entity<Proveedor>()
                .HasKey(p => p.NIT);

            modelBuilder.Entity<PagoPrima>()
                .HasKey(p => p.IdPago);

            modelBuilder.Entity<PagoPrima>()
                .HasOne<Paciente>()
                .WithMany()
                .HasForeignKey(p => p.CodigoPaciente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConsultaCobertura>()
                .HasKey(c => c.IdConsulta);

            modelBuilder.Entity<ConsultaCobertura>()
                .HasOne<Proveedor>()
                .WithMany()
                .HasForeignKey(c => c.NITProveedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsultaCobertura>()
                .HasOne<Paciente>()
                .WithMany()
                .HasForeignKey(c => c.CodigoPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.FechaInicioCobertura)
                .HasDatabaseName("IX_Pacientes_FechaInicioCobertura");

            modelBuilder.Entity<PagoPrima>()
                .HasIndex(p => new { p.CodigoPaciente, p.FechaPago })
                .HasDatabaseName("IX_PagosPrima_CodigoPaciente_FechaPago");

            modelBuilder.Entity<ConsultaCobertura>()
                .HasIndex(c => new { c.NITProveedor, c.CodigoPaciente })
                .HasDatabaseName("IX_ConsultasCobertura_NITProveedor_CodigoPaciente");
        }
    }
}
