using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public enum EstadoCita
    {
        Pendiente,
        Confirmada,
        Cancelada,
        Atendida 
    }

    public class Cita
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int ConsultorioId { get; set; }

        [Required(ErrorMessage = "El motivo de la cita es obligatorio.")]
        [StringLength(1000, ErrorMessage = "El motivo no puede superar los 1000 caracteres.")]
        public required string Motivo { get; set; }

        [EnumDataType(typeof(EstadoCita), ErrorMessage = "El estado de la cita debe ser válido.")]
        public EstadoCita Estado { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}