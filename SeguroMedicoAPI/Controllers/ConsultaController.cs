using Microsoft.AspNetCore.Mvc;
using SeguroMedicoAPI.Services;
using SeguroMedicoAPI.DTOs;

namespace SeguroMedicoAPI.Controllers
{
    //  POST /api/consulta/
    [Route("api/consulta")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaCoberturaService _consultaCoberturaService;

        public ConsultaController(IConsultaCoberturaService consultaCoberturaService)
        {
            _consultaCoberturaService = consultaCoberturaService;
        }

        // POST: api/consulta/proveedor
        [HttpPost("proveedor")]
        public async Task<ActionResult<string>> ConsultaProveedor([FromBody] ConsultaProveedorDTO consulta)
        {
            var resultado = await _consultaCoberturaService.ConsultaProveedorAsync(consulta);

            if (resultado == null)
            {
                return NotFound("Sin Cobertura");
            }

            return Ok(resultado);
        }

        // POST: api/consulta/afiliado
        [HttpPost("afiliado")]
        public async Task<ActionResult<string>> ConsultaAfiliado([FromBody] ConsultaAfiliadoDTO consulta)
        {
            var estado = await _consultaCoberturaService.ConsultaAfiliadoAsync(consulta);

            return Ok(estado ? "Activo" : "Sin Cobertura");
        }
    }
}
