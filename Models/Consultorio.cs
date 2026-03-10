using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    /// <summary>
    /// Representa un consultorio médico dentro de la clínica.
    /// Contiene información básica como el nombre y la ubicación
    /// donde se realizan las atenciones médicas.
    /// </summary>
    public class Consultorio
    {
        /// <summary>
        /// Identificador único del consultorio.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre o número identificador del consultorio.
        /// </summary>
        [Required(ErrorMessage = "El nombre del consultorio es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del consultorio no puede superar los 50 caracteres.")]
        public required string Nombre { get; set; }

        /// <summary>
        /// Ubicación física del consultorio dentro de la clínica
        /// (por ejemplo: piso, ala o sector).
        /// </summary>
        [Required(ErrorMessage = "La ubicación del consultorio es obligatoria.")]
        [StringLength(100, ErrorMessage = "La ubicación del consultorio no puede superar los 100 caracteres.")]
        public required string Ubicacion { get; set; }
    }
}