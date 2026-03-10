using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las especialidades médicas del sistema.
    /// Permite registrar, actualizar, eliminar y consultar especialidades disponibles.
    /// </summary>
    public class EspecialidadService
    {
        private readonly Dictionary<int, Especialidad> _especialidades = new();
        private int _currentId = 1;

        /// <summary>
        /// Crea una nueva especialidad médica en el sistema.
        /// </summary>
        /// <param name="especialidad">
        /// Objeto <see cref="Especialidad"/> con la información de la especialidad a registrar.
        /// El identificador será asignado automáticamente.
        /// </param>
        /// <returns>
        /// La especialidad creada con su identificador generado.
        /// </returns>
        public Especialidad Crear(Especialidad especialidad)
        {
            especialidad.Id = _currentId++;
            _especialidades[especialidad.Id] = especialidad;

            return especialidad;
        }

        /// <summary>
        /// Elimina una especialidad existente del sistema.
        /// </summary>
        /// <param name="id">Identificador único de la especialidad a eliminar.</param>
        /// <returns>
        /// <c>true</c> si la especialidad fue eliminada correctamente;
        /// <c>false</c> si no existe una especialidad con el Id especificado.
        /// </returns>
        public bool Eliminar(int id)
        {
            return _especialidades.Remove(id);
        }

        /// <summary>
        /// Actualiza la información de una especialidad existente.
        /// </summary>
        /// <param name="id">Identificador de la especialidad que se desea actualizar.</param>
        /// <param name="especialidadActualizada">
        /// Objeto <see cref="Especialidad"/> con los nuevos datos de la especialidad.
        /// </param>
        /// <returns>
        /// La especialidad actualizada si existe en el sistema;
        /// <c>null</c> si no se encuentra una especialidad con el Id especificado.
        /// </returns>
        public Especialidad? Actualizar(int id, Especialidad especialidadActualizada)
        {
            if (!_especialidades.ContainsKey(id))
                return null;

            especialidadActualizada.Id = id;
            _especialidades[id] = especialidadActualizada;

            return especialidadActualizada;
        }

        /// <summary>
        /// Obtiene una especialidad específica mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la especialidad.</param>
        /// <returns>
        /// El objeto <see cref="Especialidad"/> correspondiente al Id indicado,
        /// o <c>null</c> si la especialidad no existe.
        /// </returns>
        public Especialidad? ObtenerPorId(int id)
        {
            _especialidades.TryGetValue(id, out var especialidad);
            return especialidad;
        }

        /// <summary>
        /// Obtiene todas las especialidades registradas en el sistema.
        /// </summary>
        /// <returns>
        /// Lista completa de especialidades almacenadas.
        /// Si no existen registros, se devuelve una lista vacía.
        /// </returns>
        public List<Especialidad> ObtenerTodos()
        {
            return _especialidades.Values.ToList();
        }
    }
}