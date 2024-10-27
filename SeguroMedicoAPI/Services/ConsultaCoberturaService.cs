using Microsoft.EntityFrameworkCore;
using SeguroMedicoAPI.Data;
using SeguroMedicoAPI.DTOs;
using SeguroMedicoAPI.Models;

namespace SeguroMedicoAPI.Services
{
    public class ConsultaCoberturaService : IConsultaCoberturaService
    {
        private readonly SeguroMedicoContext _context;

        public ConsultaCoberturaService(SeguroMedicoContext context)
        {
            _context = context;
        }

        public async Task<string> ConsultaProveedorAsync(ConsultaProveedorDTO consulta)
        {
            var proveedor = await _context.Proveedores.FindAsync(consulta.NitProveedor);
            if (proveedor == null) return null;

            var paciente = await _context.Pacientes
                .Where(p => p.CodigoPaciente == consulta.CodigoPaciente && p.FechaNacimiento == consulta.FechaNacimiento)
                .FirstOrDefaultAsync();
            if (paciente == null) return null;

            var coberturaValida = await _context.PagosPrima
                .AnyAsync(p => p.CodigoPaciente == consulta.CodigoPaciente && p.MesCoberturaCancelado >= consulta.FechaCobertura);

            if (!coberturaValida) return "Sin Cobertura";

            var numeroAutorizacion = Guid.NewGuid().ToString();
            _context.ConsultasCobertura.Add(new ConsultaCobertura
            {
                NITProveedor = consulta.NitProveedor,
                CodigoPaciente = consulta.CodigoPaciente,
                FechaCoberturaConsultada = consulta.FechaCobertura,
                Respuesta = numeroAutorizacion,
                FechaConsulta = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return numeroAutorizacion;
        }

        public async Task<bool> ConsultaAfiliadoAsync(ConsultaAfiliadoDTO consulta)
        {
            var paciente = await _context.Pacientes
                .Where(p => p.CodigoPaciente == consulta.CodigoPaciente && p.FechaNacimiento == consulta.FechaNacimiento)
                .FirstOrDefaultAsync();
            if (paciente == null) return false;

            return await _context.PagosPrima
                .AnyAsync(p => p.CodigoPaciente == consulta.CodigoPaciente && p.MesCoberturaCancelado >= DateTime.Today);
        }
    }
}
