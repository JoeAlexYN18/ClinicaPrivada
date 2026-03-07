using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    public class MedicoService
    {
        private readonly Dictionary<int, Medico> _medicos = new();
        private int _currentId = 1;
        private readonly EspecialidadService _especialidadService;

        public MedicoService(EspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        public Medico? Crear(Medico medico)
        {
            if (_especialidadService.ObtenerPorId(medico.EspecialidadId) is null)
                return null;

            if (_medicos.ContainsKey(medico.Id))
                return null;

            medico.Id = _currentId++;
            _medicos[medico.Id] = medico;

            return medico;
        }

        public bool Eliminar(int id)
        {
            return _medicos.Remove(id);
        }

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

        public Medico? ObtenerPorId(int id)
        {
            _medicos.TryGetValue(id, out var medico);
            return medico;
        }

        public List<Medico> ObtenerPorEspecialidad(int especialidadId)
        {
            return _medicos.Values
                .Where(m => m.EspecialidadId == especialidadId)
                .ToList();
        }

        public List<Medico> ObtenerTodos()
        {
            return _medicos.Values.ToList();
        }
    }
}