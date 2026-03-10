using ClinicaPrivada.Models;
using ClinicaPrivada.DTOs;

namespace ClinicaPrivada.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las citas médicas del sistema.
    /// Permite crear, actualizar, eliminar y consultar citas, validando
    /// la existencia del paciente, médico y consultorio asociados.
    /// </summary>
    public class CitaService
    {
        private readonly Dictionary<int, Cita> _citas = [];
        private int _currentId = 1;

        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;
        private readonly ConsultorioService _consultorioService;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de citas.
        /// </summary>
        /// <param name="pacienteService">
        /// Servicio utilizado para validar y obtener información de pacientes.
        /// </param>
        /// <param name="medicoService">
        /// Servicio utilizado para validar y obtener información de médicos.
        /// </param>
        /// <param name="consultorioService">
        /// Servicio utilizado para validar y obtener información de consultorios.
        /// </param>
        public CitaService(
            PacienteService pacienteService,
            MedicoService medicoService,
            ConsultorioService consultorioService)
        {
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _consultorioService = consultorioService;
        }

        /// <summary>
        /// Crea una nueva cita médica en el sistema.
        /// </summary>
        /// <param name="cita">
        /// Objeto <see cref="Cita"/> con la información de la cita, incluyendo
        /// los identificadores del paciente, médico y consultorio.
        /// </param>
        /// <returns>
        /// La cita creada con su identificador y fecha de creación asignados;
        /// <c>null</c> si el paciente, médico o consultorio especificado no existen.
        /// </returns>
        public Cita? Crear(Cita cita)
        {
            if (_pacienteService.ObtenerPorId(cita.PacienteId) is null ||
                _medicoService.ObtenerPorId(cita.MedicoId) is null ||
                _consultorioService.ObtenerPorId(cita.ConsultorioId) is null)
            {
                return null;
            }

            cita.Id = _currentId++;
            cita.FechaCreacion = DateTime.UtcNow;
            _citas[cita.Id] = cita;

            return cita;
        }

        /// <summary>
        /// Actualiza la información de una cita existente.
        /// </summary>
        /// <param name="id">Identificador de la cita que se desea actualizar.</param>
        /// <param name="citaActualizada">
        /// Objeto <see cref="Cita"/> con los nuevos datos de la cita.
        /// </param>
        /// <returns>
        /// La cita actualizada si existe y las entidades relacionadas son válidas;
        /// <c>null</c> si la cita no existe o si el paciente, médico o consultorio no están registrados.
        /// </returns>
        public Cita? Actualizar(int id, Cita citaActualizada)
        {
            if (!_citas.ContainsKey(id))
                return null;

            if (_pacienteService.ObtenerPorId(citaActualizada.PacienteId) is null ||
                _medicoService.ObtenerPorId(citaActualizada.MedicoId) is null ||
                _consultorioService.ObtenerPorId(citaActualizada.ConsultorioId) is null)
            {
                return null;
            }

            citaActualizada.Id = id;
            citaActualizada.FechaCreacion = DateTime.UtcNow;
            _citas[id] = citaActualizada;

            return citaActualizada;
        }

        /// <summary>
        /// Elimina una cita médica del sistema.
        /// </summary>
        /// <param name="id">Identificador único de la cita.</param>
        /// <returns>
        /// <c>true</c> si la cita fue eliminada correctamente;
        /// <c>false</c> si no existe una cita con el Id especificado.
        /// </returns>
        public bool Eliminar(int id) => _citas.Remove(id);

        /// <summary>
        /// Obtiene una cita específica mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la cita.</param>
        /// <returns>
        /// El objeto <see cref="Cita"/> correspondiente al Id indicado,
        /// o <c>null</c> si la cita no existe.
        /// </returns>
        public Cita? ObtenerPorId(int id)
        {
            _citas.TryGetValue(id, out var cita);
            return cita;
        }

        /// <summary>
        /// Obtiene todas las citas registradas en el sistema.
        /// </summary>
        /// <returns>
        /// Lista de <see cref="CitaDTO"/> con información completa de la cita,
        /// incluyendo datos del paciente, médico y consultorio asociados.
        /// Si no existen citas, se devuelve una lista vacía.
        /// </returns>
        public List<CitaDTO> ObtenerTodos()
        {
            return _citas.Values.Select(MapToDTO).ToList();
        }

        /// <summary>
        /// Convierte una entidad <see cref="Cita"/> en un objeto <see cref="CitaDTO"/>
        /// enriquecido con información del paciente, médico y consultorio.
        /// </summary>
        /// <param name="cita">Entidad de cita que se desea transformar.</param>
        /// <returns>
        /// Objeto <see cref="CitaDTO"/> con información completa de la cita
        /// y los nombres de las entidades relacionadas.
        /// </returns>
        public CitaDTO MapToDTO(Cita cita)
        {
            var paciente = _pacienteService.ObtenerPorId(cita.PacienteId);
            var medico = _medicoService.ObtenerPorId(cita.MedicoId);
            var consultorio = _consultorioService.ObtenerPorId(cita.ConsultorioId);

            return new CitaDTO
            {
                Id = cita.Id,
                PacienteId = cita.PacienteId,
                PacienteNombres = paciente?.Nombres ?? "",
                PacienteApellidos = paciente?.Apellidos ?? "",
                MedicoId = cita.MedicoId,
                MedicoNombres = medico?.Nombres ?? "",
                MedicoApellidos = medico?.Apellidos ?? "",
                ConsultorioId = cita.ConsultorioId,
                ConsultorioNombre = consultorio?.Nombre ?? "",
                Motivo = cita.Motivo,
                Estado = cita.Estado.ToString(),
                FechaCreacion = cita.FechaCreacion
            };
        }
    }
}