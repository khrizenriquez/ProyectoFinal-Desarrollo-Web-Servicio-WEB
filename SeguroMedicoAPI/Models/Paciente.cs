namespace SeguroMedicoAPI.Models
{
    public class Paciente
    {
        public int CodigoPaciente { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaInicioCobertura { get; set; }
        public decimal MontoCobertura { get; set; }
    }
}
