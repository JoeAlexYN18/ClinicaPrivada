using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public class Especialidad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la especialidad es obligatorio.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "El nombre de la especialidad debe tener entre 1 y 50 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción de la especialidad es obligatoria.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "La descripción de la especialidad debe tener entre 1 y 1000 caracteres.")]
        public required string Descripcion { get; set; }
    }
}