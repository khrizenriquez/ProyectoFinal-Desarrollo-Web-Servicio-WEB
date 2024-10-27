using SeguroMedicoAPI.DTOs;

namespace SeguroMedicoAPI.Services
{
    public interface IConsultaCoberturaService
    {
        Task<string> ConsultaProveedorAsync(ConsultaProveedorDTO consulta);
        Task<bool> ConsultaAfiliadoAsync(ConsultaAfiliadoDTO consulta);
    }
}
