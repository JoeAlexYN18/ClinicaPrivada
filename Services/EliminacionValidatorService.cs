namespace ClinicaPrivada.Services
{
    /// <summary>
    /// Servicio encargado de validar si ciertas entidades del sistema
    /// pueden ser eliminadas sin afectar la integridad de los datos.
    /// Verifica si existen relaciones activas con citas o médicos.
    /// </summary>
    public class EliminacionValidatorService
    {
        private readonly CitaService _citaService;
        private readonly MedicoService _medicoService;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de validación de eliminaciones.
        /// </summary>
        /// <param name="citaService">
        /// Servicio utilizado para verificar si existen citas relacionadas con una entidad.
        /// </param>
        /// <param name="medicoService">
        /// Servicio utilizado para verificar si existen médicos asociados a una especialidad.
        /// </param>
        public EliminacionValidatorService(CitaService citaService, MedicoService medicoService)
        {
            _citaService = citaService;
            _medicoService = medicoService;
        }

        /// <summary>
        /// Determina si un paciente puede ser eliminado del sistema.
        /// </summary>
        /// <param name="pacienteId">Identificador único del paciente.</param>
        /// <returns>
        /// <c>true</c> si el paciente no tiene citas asociadas y puede eliminarse;
        /// <c>false</c> si existen citas registradas con ese paciente.
        /// </returns>
        public bool PacientePuedeEliminarse(int pacienteId)
        {
            return !_citaService.ObtenerTodos().Any(c => c.PacienteId == pacienteId);
        }

        /// <summary>
        /// Determina si un médico puede ser eliminado del sistema.
        /// </summary>
        /// <param name="medicoId">Identificador único del médico.</param>
        /// <returns>
        /// <c>true</c> si el médico no tiene citas asociadas y puede eliminarse;
        /// <c>false</c> si existen citas registradas con ese médico.
        /// </returns>
        public bool MedicoPuedeEliminarse(int medicoId)
        {
            return !_citaService.ObtenerTodos().Any(c => c.MedicoId == medicoId);
        }

        /// <summary>
        /// Determina si un consultorio puede ser eliminado del sistema.
        /// </summary>
        /// <param name="consultorioId">Identificador único del consultorio.</param>
        /// <returns>
        /// <c>true</c> si el consultorio no tiene citas asociadas y puede eliminarse;
        /// <c>false</c> si existen citas registradas que utilizan ese consultorio.
        /// </returns>
        public bool ConsultorioPuedeEliminarse(int consultorioId)
        {
            return !_citaService.ObtenerTodos().Any(c => c.ConsultorioId == consultorioId);
        }

        /// <summary>
        /// Determina si una especialidad médica puede ser eliminada del sistema.
        /// </summary>
        /// <param name="especialidadId">Identificador único de la especialidad.</param>
        /// <returns>
        /// <c>true</c> si no existen médicos asociados a esa especialidad;
        /// <c>false</c> si uno o más médicos están registrados con dicha especialidad.
        /// </returns>
        public bool EspecialidadPuedeEliminarse(int especialidadId)
        {
            return !_medicoService.ObtenerTodos().Any(m => m.EspecialidadId == especialidadId);
        }
    }
}