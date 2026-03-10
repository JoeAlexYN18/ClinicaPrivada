using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las operaciones relacionadas con los médicos.
    /// Permite crear, actualizar, eliminar y consultar médicos, validando la existencia
    /// de la especialidad asociada a cada uno.
    /// </summary>
    public class MedicoService
    {
        private readonly Dictionary<int, Medico> _medicos = [];
        private int _currentId = 1;
        private readonly EspecialidadService _especialidadService;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de médicos.
        /// </summary>
        /// <param name="especialidadService">
        /// Servicio utilizado para validar la existencia de especialidades médicas.
        /// </param>
        public MedicoService(EspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        /// <summary>
        /// Crea un nuevo médico en el sistema.
        /// </summary>
        /// <param name="medico">
        /// Objeto <see cref="Medico"/> con la información del médico, incluyendo el Id de su especialidad.
        /// </param>
        /// <returns>
        /// El médico creado con su identificador asignado si la especialidad existe;
        /// <c>null</c> si la especialidad especificada no se encuentra registrada.
        /// </returns>
        public Medico? Crear(Medico medico)
        {
            if (_especialidadService.ObtenerPorId(medico.EspecialidadId) is null)
                return null;

            medico.Id = _currentId++;
            _medicos[medico.Id] = medico;

            return medico;
        }

        /// <summary>
        /// Elimina un médico existente del sistema.
        /// </summary>
        /// <param name="id">Identificador único del médico a eliminar.</param>
        /// <returns>
        /// <c>true</c> si el médico fue eliminado correctamente;
        /// <c>false</c> si no existe un médico con el Id especificado.
        /// </returns>
        public bool Eliminar(int id)
        {
            return _medicos.Remove(id);
        }

        /// <summary>
        /// Actualiza la información de un médico existente.
        /// </summary>
        /// <param name="id">Identificador del médico que se desea actualizar.</param>
        /// <param name="medicoActualizado">
        /// Objeto <see cref="Medico"/> con los nuevos datos del médico.
        /// </param>
        /// <returns>
        /// El médico actualizado si el registro existe y la especialidad es válida;
        /// <c>null</c> si el médico no existe o si la especialidad indicada no está registrada.
        /// </returns>
        public Medico? Actualizar(int id, Medico medicoActualizado)
        {
            if (!_medicos.ContainsKey(id))
                return null;

            if (_especialidadService.ObtenerPorId(medicoActualizado.EspecialidadId) is null)
                return null;

            medicoActualizado.Id = id;
            _medicos[id] = medicoActualizado;

            return medicoActualizado;
        }

        /// <summary>
        /// Obtiene un médico específico mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único del médico.</param>
        /// <returns>
        /// El objeto <see cref="Medico"/> correspondiente al Id indicado,
        /// o <c>null</c> si el médico no existe.
        /// </returns>
        public Medico? ObtenerPorId(int id)
        {
            _medicos.TryGetValue(id, out var medico);
            return medico;
        }

        /// <summary>
        /// Obtiene una lista de médicos que pertenecen a una especialidad específica.
        /// </summary>
        /// <param name="especialidadId">Identificador de la especialidad médica.</param>
        /// <returns>
        /// Lista de médicos que tienen asignada la especialidad indicada.
        /// Si no existen coincidencias, se devuelve una lista vacía.
        /// </returns>
        public List<Medico> ObtenerPorEspecialidad(int especialidadId)
        {
            return _medicos.Values
                .Where(m => m.EspecialidadId == especialidadId)
                .ToList();
        }

        /// <summary>
        /// Obtiene todos los médicos registrados en el sistema.
        /// </summary>
        /// <returns>
        /// Lista completa de médicos almacenados.
        /// Si no existen registros, se devuelve una lista vacía.
        /// </returns>
        public List<Medico> ObtenerTodos()
        {
            return _medicos.Values.ToList();
        }
    }
}