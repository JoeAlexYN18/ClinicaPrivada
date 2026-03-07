using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres.")]
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Los apellidos deben tener entre 1 y 100 caracteres.")]
        public required string Apellidos { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "El sexo debe tener entre 1 y 20 caracteres.")]
        public required string Sexo { get; set; }

        [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
        [RegularExpression(@"^\d{8,20}$", ErrorMessage = "El documento de identidad debe tener entre 8 y 20 dígitos.")]
        public required string DocumentoIdentidad { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "FechaNacimiento debe tener un formato de fecha válido (YYYY-MM-DD).")]
        public required DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{9,20}$", ErrorMessage = "El teléfono debe tener entre 9 y 20 dígitos.")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email debe tener un formato válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio.")]
        [Range(1, 300, ErrorMessage = "El peso debe estar entre 1 y 300 kg.")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "La estatura es obligatoria.")]
        [Range(30, 250, ErrorMessage = "La estatura debe estar entre 30 y 250 cm.")]
        public int Estatura { get; set; }
    }
}