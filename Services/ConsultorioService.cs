using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    public class ConsultorioService
    {
        private readonly Dictionary<int, Consultorio> _consultorios = new();
        private int _currentId = 1;

        public Consultorio Crear(Consultorio consultorio)
        {

            consultorio.Id = _currentId++;
            _consultorios[consultorio.Id] = consultorio;

            return consultorio;
        }

        public bool Eliminar(int id)
        {
            return _consultorios.Remove(id);
        }

        public Consultorio? Actualizar(int id, Consultorio consultorioActualizado)
        {
            if (!_consultorios.ContainsKey(id))
                return null;

            consultorioActualizado.Id = id;
            _consultorios[id] = consultorioActualizado;

            return consultorioActualizado;
        }

        public Consultorio? ObtenerPorId(int id)
        {
            _consultorios.TryGetValue(id, out var consultorio);
            return consultorio;
        }

        public List<Consultorio> ObtenerTodos()
        {
            return _consultorios.Values.ToList();
        }
    }
}