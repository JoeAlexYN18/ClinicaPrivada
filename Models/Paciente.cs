using System.ComponentModel.DataAnnotations;

namespace ClinicaPrivada.Models
{
    /// <summary>
    /// Representa los posibles sexos que un paciente puede tener.
    /// </summary>
    public enum Sexo
    {
        /// <summary>
        /// Paciente de sexo masculino.
        /// </summary>
        Masculino,

        /// <summary>
        /// Paciente de sexo femenino.
        /// </summary>
        Femenino,

        /// <summary>
        /// Paciente que se identifica con otro sexo o prefiere no especificar.
        /// </summary>
        Otro
    }

    /// <summary>
    /// Representa a un paciente registrado en la clínica.
    /// Contiene información personal, contacto y medidas físicas.
    /// </summary>
    public class Paciente
    {
        /// <summary>
        /// Identificador único del paciente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombres del paciente.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public required string Nombres { get; set; }

        /// <summary>
        /// Apellidos del paciente.
        /// </summary>
        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden superar los 100 caracteres.")]
        public required string Apellidos { get; set; }

        /// <summary>
        /// Sexo del paciente.
        /// </summary>
        [EnumDataType(typeof(Sexo), ErrorMessage = "El sexo debe ser válido.")]
        public Sexo Sexo { get; set; }

        /// <summary>
        /// Documento de identidad del paciente (exactamente 8 dígitos).
        /// </summary>
        [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El documento de identidad debe tener exactamente 8 dígitos.")]
        public required string DocumentoIdentidad { get; set; }

        /// <summary>
        /// Fecha de nacimiento del paciente.
        /// </summary>
        public required DateOnly FechaNacimiento { get; set; }

        /// <summary>
        /// Número de teléfono del paciente (exactamente 9 dígitos).
        /// </summary>
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe tener exactamente 9 dígitos.")]
        public required string Telefono { get; set; }

        /// <summary>
        /// Correo electrónico del paciente.
        /// </summary>
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email debe tener un formato válido.")]
        public required string Email { get; set; }

        /// <summary>
        /// Peso del paciente en kilogramos. Debe estar entre 1 y 300 kg.
        /// </summary>
        [Range(1, 300, ErrorMessage = "El peso debe estar entre 1 y 300 kg.")]
        public double Peso { get; set; }

        /// <summary>
        /// Estatura del paciente en centímetros. Debe estar entre 30 y 250 cm.
        /// </summary>
        [Range(30, 250, ErrorMessage = "La estatura debe estar entre 30 y 250 cm.")]
        public int Estatura { get; set; }
    }
}