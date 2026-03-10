using ClinicaPrivada.Models;

namespace ClinicaPrivada.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las operaciones relacionadas con los pacientes.
    /// Maneja la creación, actualización, eliminación y consulta de pacientes en memoria.
    /// </summary>
    public class PacienteService
    {
        private readonly Dictionary<int, Paciente> _pacientes = [];
        private int _currentId = 1;

        /// <summary>
        /// Crea un nuevo paciente en el sistema.
        /// </summary>
        /// <param name="paciente">
        /// Objeto <see cref="Paciente"/> con la información del paciente a registrar.
        /// El Id será generado automáticamente por el sistema.
        /// </param>
        /// <returns>
        /// El objeto <see cref="Paciente"/> creado con su identificador asignado.
        /// </returns>
        public Paciente Crear(Paciente paciente)
        {
            paciente.Id = _currentId++;
            _pacientes[paciente.Id] = paciente;

            return paciente;
        }

        /// <summary>
        /// Elimina un paciente existente del sistema.
        /// </summary>
        /// <param name="id">Identificador único del paciente a eliminar.</param>
        /// <returns>
        /// <c>true</c> si el paciente fue eliminado correctamente;
        /// <c>false</c> si no existe un paciente con el Id especificado.
        /// </returns>
        public bool Eliminar(int id)
        {
            return _pacientes.Remove(id);
        }

        /// <summary>
        /// Actualiza la información de un paciente existente.
        /// </summary>
        /// <param name="id">Identificador del paciente que se desea actualizar.</param>
        /// <param name="pacienteActualizado">
        /// Objeto <see cref="Paciente"/> con los nuevos datos del paciente.
        /// </param>
        /// <returns>
        /// El paciente actualizado si existe en el sistema;
        /// <c>null</c> si no se encontró un paciente con el Id especificado.
        /// </returns>
        public Paciente? Actualizar(int id, Paciente pacienteActualizado)
        {
            if (!_pacientes.ContainsKey(id))
                return null;

            pacienteActualizado.Id = id;
            _pacientes[id] = pacienteActualizado;

            return pacienteActualizado;
        }

        /// <summary>
        /// Obtiene un paciente específico mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único del paciente.</param>
        /// <returns>
        /// El objeto <see cref="Paciente"/> correspondiente al Id indicado,
        /// o <c>null</c> si el paciente no existe.
        /// </returns>
        public Paciente? ObtenerPorId(int id)
        {
            _pacientes.TryGetValue(id, out var paciente);
            return paciente;
        }

        /// <summary>
        /// Obtiene una lista de pacientes filtrados por sexo.
        /// </summary>
        /// <param name="sexo">Valor del enum <see cref="Sexo"/> utilizado para filtrar los pacientes.</param>
        /// <returns>
        /// Lista de pacientes cuyo sexo coincide con el valor especificado.
        /// Si no existen coincidencias, se devuelve una lista vacía.
        /// </returns>
        public List<Paciente> ObtenerPorSexo(Sexo sexo)
        {
            return _pacientes.Values
                .Where(p => p.Sexo == sexo)
                .ToList();
        }

        /// <summary>
        /// Obtiene todos los pacientes registrados en el sistema.
        /// </summary>
        /// <returns>
        /// Lista completa de pacientes almacenados.
        /// Si no existen pacientes, se devuelve una lista vacía.
        /// </returns>
        public List<Paciente> ObtenerTodos()
        {
            return _pacientes.Values.ToList();
        }
    }
}