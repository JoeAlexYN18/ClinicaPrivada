using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    /// <summary>
    /// Representa una especialidad médica dentro de la clínica.
    /// Contiene información básica como el nombre de la especialidad
    /// y una descripción detallada de la misma.
    /// </summary>
    public class Especialidad
    {
        /// <summary>
        /// Identificador único de la especialidad.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la especialidad (por ejemplo: Cardiología, Pediatría).
        /// </summary>
        [Required(ErrorMessage = "El nombre de la especialidad es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de la especialidad no puede superar los 50 caracteres.")]
        public required string Nombre { get; set; }

        /// <summary>
        /// Descripción detallada de la especialidad, incluyendo alcance y objetivos.
        /// </summary>
        [Required(ErrorMessage = "La descripción de la especialidad es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción de la especialidad no puede superar los 1000 caracteres.")]
        public required string Descripcion { get; set; }
    }
}

