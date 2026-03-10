using System.Text.Json.Serialization;

namespace ClinicaPrivada.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) para representar información completa de una cita médica.
    /// Incluye datos del paciente, médico, consultorio, motivo, estado y fecha de creación.
    /// </summary>
    public class CitaDTO
    {
        /// <summary>
        /// Identificador único de la cita.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador del paciente asociado a la cita.
        /// </summary>
        public int PacienteId { get; set; }

        /// <summary>
        /// Nombres del paciente.
        /// </summary>
        public string PacienteNombres { get; set; } = "";

        /// <summary>
        /// Apellidos del paciente.
        /// </summary>
        public string PacienteApellidos { get; set; } = "";

        /// <summary>
        /// Identificador del médico que atenderá la cita.
        /// </summary>
        public int MedicoId { get; set; }

        /// <summary>
        /// Nombres del médico.
        /// </summary>
        public string MedicoNombres { get; set; } = "";

        /// <summary>
        /// Apellidos del médico.
        /// </summary>
        public string MedicoApellidos { get; set; } = "";

        /// <summary>
        /// Identificador del consultorio donde se realizará la cita.
        /// </summary>
        public int ConsultorioId { get; set; }

        /// <summary>
        /// Nombre del consultorio donde se realizará la cita.
        /// </summary>
        public string ConsultorioNombre { get; set; } = "";

        /// <summary>
        /// Motivo o descripción de la cita.
        /// </summary>
        public string Motivo { get; set; } = "";

        /// <summary>
        /// Estado actual de la cita (Pendiente, Confirmada, Cancelada, Atendida).
        /// </summary>
        public string Estado { get; set; } = "";

        /// <summary>
        /// Fecha y hora en la que se creó la cita.
        /// </summary>
        [JsonIgnore]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de creación en formato "YYYY-MM-DD HH:mm:ss" en hora local de Perú
        /// </summary>
        [JsonPropertyName("FechaCreacion")]
        public string FechaCreacionFormateada
        {
            get
            {
                var peruZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); 
                var localTime = TimeZoneInfo.ConvertTimeFromUtc(FechaCreacion, peruZone);
                return localTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}