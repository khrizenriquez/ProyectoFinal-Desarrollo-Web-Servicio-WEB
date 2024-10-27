using Microsoft.Data.SqlClient;
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
            string respuestaHttp;
            string numeroAutorizacion = null;

            var proveedor = await _context.Proveedores.FindAsync(consulta.NitProveedor);
            if (proveedor == null)
            {
                respuestaHttp = "404 Proveedor no encontrado";
                return respuestaHttp;
            }

            var paciente = await _context.Pacientes
                .Where(p => p.CodigoPaciente == consulta.CodigoPaciente && p.FechaNacimiento == consulta.FechaNacimiento)
                .FirstOrDefaultAsync();
            if (paciente == null)
            {
                respuestaHttp = "404 Paciente no encontrado";
                return respuestaHttp;
            }

            var coberturaValida = await _context.PagosPrima
                .AnyAsync(p => p.CodigoPaciente == consulta.CodigoPaciente && p.MesCoberturaCancelado >= consulta.FechaCobertura);

            if (coberturaValida)
            {
                numeroAutorizacion = Guid.NewGuid().ToString();
                respuestaHttp = "200 OK";
            }
            else
            {
                respuestaHttp = "404 Sin Cobertura";
            }

            if (coberturaValida)
            {
                var consultaCobertura = new ConsultaCobertura
                {
                    NITProveedor = consulta.NitProveedor,
                    CodigoPaciente = consulta.CodigoPaciente,
                    FechaCoberturaConsultada = consulta.FechaCobertura,
                    Respuesta = respuestaHttp,
                    FechaConsulta = DateTime.Now,
                    NumeroAutorizacion = numeroAutorizacion
                };

                _context.ConsultasCobertura.Add(consultaCobertura);
                await _context.SaveChangesAsync();
            }

            return numeroAutorizacion ?? respuestaHttp;
        }

        private void GuardarConsulta(ConsultaProveedorDTO consulta, string respuestaHttp, string numeroAutorizacion)
        {
            var consultaCobertura = new ConsultaCobertura
            {
                NITProveedor = consulta.NitProveedor,
                CodigoPaciente = consulta.CodigoPaciente,
                FechaCoberturaConsultada = consulta.FechaCobertura,
                Respuesta = respuestaHttp,
                FechaConsulta = DateTime.Now,
                NumeroAutorizacion = numeroAutorizacion
            };

            _context.ConsultasCobertura.Add(consultaCobertura);
            _context.SaveChanges();
        }

        private void GuardarConsultaSinProveedor(ConsultaProveedorDTO consulta, string respuestaHttp)
        {
            var consultaCobertura = new ConsultaCobertura
            {
                NITProveedor = "No encontrado",
                CodigoPaciente = consulta.CodigoPaciente,
                FechaCoberturaConsultada = consulta.FechaCobertura,
                Respuesta = respuestaHttp,
                FechaConsulta = DateTime.Now,
                NumeroAutorizacion = null
            };

            _context.ConsultasCobertura.Add(consultaCobertura);
            _context.SaveChanges();
        }

        public async Task<bool> ConsultaAfiliadoAsync(ConsultaAfiliadoDTO consulta)
        {
            var paciente = await _context.Pacientes
                .Where(p => p.CodigoPaciente == consulta.CodigoPaciente && p.FechaNacimiento == consulta.FechaNacimiento)
                .FirstOrDefaultAsync();

            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado");
                return false;
            }

            bool tieneCobertura = await _context.PagosPrima
                .AnyAsync(p => p.CodigoPaciente == consulta.CodigoPaciente && p.MesCoberturaCancelado >= DateTime.Today);

            if (!tieneCobertura)
            {
                Console.WriteLine("Sin Cobertura: No se encontró un pago de prima con cobertura para la fecha actual.");
            }

            return tieneCobertura;
        }
    }
}
