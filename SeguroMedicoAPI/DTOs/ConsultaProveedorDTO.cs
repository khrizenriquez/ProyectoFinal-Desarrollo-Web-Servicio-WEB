namespace SeguroMedicoAPI.DTOs
{
    public class ConsultaProveedorDTO
    {
        public string NitProveedor { get; set; }
        public int CodigoPaciente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaCobertura { get; set; }
    }
}
