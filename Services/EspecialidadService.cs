using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    public class EspecialidadService
    {
        private readonly Dictionary<int, Especialidad> _especialidades = new();
        private int _currentId = 1;

        public Especialidad Crear(Especialidad especialidad)
        {

            especialidad.Id = _currentId++;
            _especialidades[especialidad.Id] = especialidad;

            return especialidad;
        }

        public bool Eliminar(int id)
        {
            return _especialidades.Remove(id);
        }

        public Especialidad? Actualizar(int id, Especialidad especialidadActualizada)
        {
            if (!_especialidades.ContainsKey(id))
                return null;

            especialidadActualizada.Id = id;
            _especialidades[id] = especialidadActualizada;

            return especialidadActualizada;
        }

        public Especialidad? ObtenerPorId(int id)
        {
            _especialidades.TryGetValue(id, out var especialidad);
            return especialidad;
        }

        public List<Especialidad> ObtenerTodos()
        {
            return _especialidades.Values.ToList();
        }
    }
}