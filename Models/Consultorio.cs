using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public class Consultorio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del consultorio es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del consultorio no puede superar los 50 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La ubicación del consultorio es obligatoria.")]
        [StringLength(100, ErrorMessage = "La ubicación del consultorio no puede superar los 100 caracteres.")]
        public required string Ubicacion { get; set; }
    }
}