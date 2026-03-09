using ClinicaPrivada.Models;
using ClinicaPrivada.DTOs;

namespace ClinicaPrivada.Services
{
    public class CitaService
    {
        private readonly Dictionary<int, Cita> _citas = [];
        private int _currentId = 1;

        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;
        private readonly ConsultorioService _consultorioService;

        public CitaService(
            PacienteService pacienteService,
            MedicoService medicoService,
            ConsultorioService consultorioService)
        {
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _consultorioService = consultorioService;
        }

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
            citaActualizada.FechaCreacion = _citas[id].FechaCreacion;
            _citas[id] = citaActualizada;

            return citaActualizada;
        }

        public bool Eliminar(int id) => _citas.Remove(id);

        public Cita? ObtenerPorId(int id)
        {
            _citas.TryGetValue(id, out var cita);
            return cita;
        }

        public List<CitaDTO> ObtenerTodos()
        {
            return _citas.Values.Select(MapToDTO).ToList();
        }

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