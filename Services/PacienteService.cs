using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    public class PacienteService
    {
        private readonly Dictionary<int, Paciente> _pacientes = [];
        private int _currentId = 1;

        public Paciente? Crear(Paciente paciente)
        {
            if (_pacientes.ContainsKey(paciente.Id))
                return null;

            paciente.Id = _currentId++;
            _pacientes[paciente.Id] = paciente;

            return paciente;
        }

        public bool Eliminar(int id)
        {
            return _pacientes.Remove(id);
        }

        public Paciente? Actualizar(int id, Paciente pacienteActualizado)
        {
            if (!_pacientes.ContainsKey(id))
                return null;

            pacienteActualizado.Id = id;
            _pacientes[id] = pacienteActualizado;

            return pacienteActualizado;
        }

        public Paciente? ObtenerPorId(int id)
        {
            _pacientes.TryGetValue(id, out var paciente);
            return paciente;
        }

        public List<Paciente> ObtenerPorSexo(string sexo)
        {
            return _pacientes.Values
                .Where(p => p.Sexo.Equals(sexo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Paciente> ObtenerTodos()
        {
            return _pacientes.Values.ToList();
        }
    }
}