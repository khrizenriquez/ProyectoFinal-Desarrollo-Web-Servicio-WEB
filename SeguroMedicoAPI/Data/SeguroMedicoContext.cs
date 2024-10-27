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
    }
}
