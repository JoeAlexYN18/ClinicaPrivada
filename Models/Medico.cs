using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public class Medico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre del médico debe tener entre 1 y 100 caracteres.")]
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido del médico es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El apellido del médico debe tener entre 1 y 100 caracteres.")]
        public required string Apellidos { get; set; }

        [Required(ErrorMessage = "El número de licencia es obligatorio.")]
        [StringLength(20, MinimumLength = 11, ErrorMessage = "El número de licencia debe tener entre 11 y 20 caracteres.")]
        public required string NumeroLicencia { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        public int EspecialidadId { get; set; }
    }
}