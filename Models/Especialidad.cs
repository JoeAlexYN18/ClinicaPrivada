using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public class Especialidad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la especialidad es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de la especialidad no puede superar los 50 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción de la especialidad es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción de la especialidad no puede superar los 1000 caracteres.")]
        public required string Descripcion { get; set; }
    }
}