using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public enum EstadoCita
    {
        Pendiente = 1,
        Confirmada = 2,
        Cancelada = 3,
        Atendida = 4
    }

    public class Cita
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El PacienteId es obligatorio.")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "El MedicoId es obligatorio.")]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "El ConsultorioId es obligatorio.")]
        public int ConsultorioId { get; set; }

        [Required(ErrorMessage = "El motivo de la cita es obligatorio.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "El motivo debe tener entre 1 y 1000 caracteres.")]
        public required string Motivo { get; set; }

        [Required(ErrorMessage = "El estado de la cita es obligatorio.")]
        [Range(1, 4, ErrorMessage = "El estado debe ser válido (1: Pendiente, 2: Confirmada, 3: Cancelada, 4: Atendida).")]
        public EstadoCita Estado { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}