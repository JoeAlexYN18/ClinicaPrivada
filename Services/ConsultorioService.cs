using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    /// <summary>
    /// Servicio encargado de gestionar los consultorios médicos del sistema.
    /// Permite registrar, actualizar, eliminar y consultar los consultorios disponibles.
    /// </summary>
    public class ConsultorioService
    {
        private readonly Dictionary<int, Consultorio> _consultorios = new();
        private int _currentId = 1;

        /// <summary>
        /// Crea un nuevo consultorio en el sistema.
        /// </summary>
        /// <param name="consultorio">
        /// Objeto <see cref="Consultorio"/> con la información del consultorio a registrar.
        /// El identificador será asignado automáticamente por el sistema.
        /// </param>
        /// <returns>
        /// El consultorio creado con su identificador generado.
        /// </returns>
        public Consultorio Crear(Consultorio consultorio)
        {
            consultorio.Id = _currentId++;
            _consultorios[consultorio.Id] = consultorio;

            return consultorio;
        }

        /// <summary>
        /// Elimina un consultorio existente del sistema.
        /// </summary>
        /// <param name="id">Identificador único del consultorio a eliminar.</param>
        /// <returns>
        /// <c>true</c> si el consultorio fue eliminado correctamente;
        /// <c>false</c> si no existe un consultorio con el Id especificado.
        /// </returns>
        public bool Eliminar(int id)
        {
            return _consultorios.Remove(id);
        }

        /// <summary>
        /// Actualiza la información de un consultorio existente.
        /// </summary>
        /// <param name="id">Identificador del consultorio que se desea actualizar.</param>
        /// <param name="consultorioActualizado">
        /// Objeto <see cref="Consultorio"/> con los nuevos datos del consultorio.
        /// </param>
        /// <returns>
        /// El consultorio actualizado si existe en el sistema;
        /// <c>null</c> si no se encuentra un consultorio con el Id especificado.
        /// </returns>
        public Consultorio? Actualizar(int id, Consultorio consultorioActualizado)
        {
            if (!_consultorios.ContainsKey(id))
                return null;

            consultorioActualizado.Id = id;
            _consultorios[id] = consultorioActualizado;

            return consultorioActualizado;
        }

        /// <summary>
        /// Obtiene un consultorio específico mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único del consultorio.</param>
        /// <returns>
        /// El objeto <see cref="Consultorio"/> correspondiente al Id indicado,
        /// o <c>null</c> si el consultorio no existe.
        /// </returns>
        public Consultorio? ObtenerPorId(int id)
        {
            _consultorios.TryGetValue(id, out var consultorio);
            return consultorio;
        }

        /// <summary>
        /// Obtiene todos los consultorios registrados en el sistema.
        /// </summary>
        /// <returns>
        /// Lista completa de consultorios almacenados.
        /// Si no existen registros, se devuelve una lista vacía.
        /// </returns>
        public List<Consultorio> ObtenerTodos()
        {
            return _consultorios.Values.ToList();
        }
    }
}