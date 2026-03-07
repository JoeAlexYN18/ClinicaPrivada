namespace ClinicaPrivada.DTOs
{
    public class CitaDTO
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombres { get; set; } = "";
        public string PacienteApellidos { get; set; } = "";
        public int MedicoId { get; set; }
        public string MedicoNombres { get; set; } = "";
        public string MedicoApellidos { get; set; } = "";
        public int ConsultorioId { get; set; }
        public string ConsultorioNombre { get; set; } = "";
        public string Motivo { get; set; } = "";
        public string Estado { get; set; } = "";
        public DateTime FechaCreacion { get; set; }
    }
}