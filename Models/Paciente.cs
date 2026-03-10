using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    public enum Sexo
    {
        Masculino,
        Femenino,
        Otro
    }
    
    public class Paciente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden superar los 100 caracteres.")]
        public required string Apellidos { get; set; }

        [EnumDataType(typeof(Sexo), ErrorMessage = "El sexo debe ser válido.")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El documento de identidad debe tener exactamente 8 dígitos.")]
        public required string DocumentoIdentidad { get; set; }

        public required DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe tener exactamente 9 dígitos.")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email debe tener un formato válido.")]
        public required string Email { get; set; }

        [Range(1, 300, ErrorMessage = "El peso debe estar entre 1 y 300 kg.")]
        public double Peso { get; set; }

        [Range(30, 250, ErrorMessage = "La estatura debe estar entre 30 y 250 cm.")]
        public int Estatura { get; set; }
    }
}