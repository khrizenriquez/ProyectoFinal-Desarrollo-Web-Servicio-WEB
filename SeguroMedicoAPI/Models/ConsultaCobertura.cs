namespace SeguroMedicoAPI.Models
{
    public class ConsultaCobertura
    {
        public int IdConsulta { get; set; }
        public string NITProveedor { get; set; }
        public int CodigoPaciente { get; set; }
        public DateTime FechaCoberturaConsultada { get; set; }
        public string Respuesta { get; set; }
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
        public string NumeroAutorizacion { get; set; }
    }
}
