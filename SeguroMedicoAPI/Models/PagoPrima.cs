namespace SeguroMedicoAPI.Models
{
    public class PagoPrima
    {
        public int IdPago { get; set; }
        public int CodigoPaciente { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime MesCoberturaCancelado { get; set; }
        public decimal Monto { get; set; }
    }
}
