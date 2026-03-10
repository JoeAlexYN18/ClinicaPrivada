using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    /// <summary>
    /// Representa a un médico registrado en la clínica.
    /// Contiene información personal, número de licencia y la especialidad asociada.
    /// </summary>
    public class Medico
    {
        /// <summary>
        /// Identificador único del médico.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombres del médico.
        /// </summary>
        [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre del médico debe tener entre 1 y 100 caracteres.")]
        public required string Nombres { get; set; }

        /// <summary>
        /// Apellidos del médico.
        /// </summary>
        [Required(ErrorMessage = "El apellido del médico es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El apellido del médico debe tener entre 1 y 100 caracteres.")]
        public required string Apellidos { get; set; }

        /// <summary>
        /// Número de licencia profesional del médico (exactamente 10 dígitos).
        /// </summary>
        [Required(ErrorMessage = "El número de licencia es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de licencia debe tener exactamente 10 dígitos.")]
        public required string NumeroLicencia { get; set; }

        /// <summary>
        /// Identificador de la especialidad asociada al médico.
        /// Debe corresponder a un registro válido en <see cref="Especialidad"/>.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad válida.")]
        public int EspecialidadId { get; set; }
    }
}