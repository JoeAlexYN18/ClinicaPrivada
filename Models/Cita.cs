using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    /// <summary>
    /// Representa los posibles estados de una cita médica dentro del sistema.
    /// </summary>
    public enum EstadoCita
    {
        /// <summary>
        /// La cita ha sido registrada pero aún no ha sido confirmada.
        /// </summary>
        Pendiente,

        /// <summary>
        /// La cita ha sido confirmada y está programada para realizarse.
        /// </summary>
        Confirmada,

        /// <summary>
        /// La cita ha sido cancelada y no se llevará a cabo.
        /// </summary>
        Cancelada,

        /// <summary>
        /// La cita fue atendida y completada exitosamente.
        /// </summary>
        Atendida
    }

    /// <summary>
    /// Representa una cita médica dentro del sistema de la clínica.
    /// Contiene la información necesaria para relacionar un paciente,
    /// un médico y un consultorio, así como los detalles de la atención.
    /// </summary>
    public class Cita
    {
        /// <summary>
        /// Identificador único de la cita.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador del paciente asociado a la cita.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "El ID del paciente debe ser válido.")]
        public int PacienteId { get; set; }

        /// <summary>
        /// Identificador del médico que atenderá la cita.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "El ID del médico debe ser válido.")]
        public int MedicoId { get; set; }

        /// <summary>
        /// Identificador del consultorio donde se realizará la cita.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "El ID del consultorio debe ser válido.")]
        public int ConsultorioId { get; set; }

        /// <summary>
        /// Motivo o descripción de la consulta médica.
        /// </summary>
        [Required(ErrorMessage = "El motivo de la cita es obligatorio.")]
        [StringLength(1000, ErrorMessage = "El motivo no puede superar los 1000 caracteres.")]
        public required string Motivo { get; set; }

        /// <summary>
        /// Estado actual de la cita médica.
        /// </summary>
        [EnumDataType(typeof(EstadoCita), ErrorMessage = "El estado de la cita debe ser válido.")]
        public EstadoCita Estado { get; set; }

        /// <summary>
        /// Fecha y hora en la que se registró la cita en el sistema.
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}