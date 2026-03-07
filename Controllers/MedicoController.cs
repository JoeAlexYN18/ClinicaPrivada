using ClinicaPrivada.Models;
using ClinicaPrivada.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaPrivada.Controllers
{
    /// <summary>
    /// Controlador para gestionar médicos.
    /// Permite crear, actualizar, eliminar y consultar médicos.
    /// La ID se asigna automáticamente y la especialidad debe existir al crear o actualizar.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly MedicoService _service;
        private readonly EliminacionValidatorService _eliminacionValidator;

        public MedicoController(MedicoService service, EliminacionValidatorService eliminacionValidator)
        {
            _service = service;
            _eliminacionValidator = eliminacionValidator;
        }

        /// <summary>
        /// Crea un nuevo médico.
        /// </summary>
        /// <param name="medico">Objeto médico con datos requeridos, incluyendo EspecialidadId existente.</param>
        /// <returns>El médico creado con ID asignada.</returns>
        /// <response code="201">Médico creado exitosamente.</response>
        /// <response code="400">No se puede crear el médico. Verifica que la especialidad exista y que los datos sean correctos.</response>
        [HttpPost]
        public ActionResult<Medico> Crear([FromBody] Medico medico)
        {
            var creado = _service.Crear(medico);

            if (creado is null)
                return BadRequest("No se puede crear el médico. Verifica que la especialidad exista y que los datos sean correctos.");

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        /// <summary>
        /// Elimina un médico por ID.
        /// Solo se permite si el médico no tiene citas asociadas.
        /// </summary>
        /// <param name="id">ID del médico a eliminar.</param>
        /// <response code="204">Médico eliminado correctamente.</response>
        /// <response code="404">No se encontró el médico con el ID proporcionado.</response>
        /// <response code="400">No se puede eliminar el médico porque tiene citas asociadas.</response>
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var medico = _service.ObtenerPorId(id);
            if (medico is null)
                return NotFound();

            if (!_eliminacionValidator.MedicoPuedeEliminarse(id))
                return BadRequest("No se puede eliminar el médico porque tiene citas asociadas.");

            _service.Eliminar(id);
            return NoContent();
        }

        /// <summary>
        /// Actualiza los datos de un médico existente.
        /// </summary>
        /// <param name="id">ID del médico a actualizar.</param>
        /// <param name="medico">Objeto médico con datos actualizados, incluyendo EspecialidadId existente.</param>
        /// <response code="200">Médico actualizado correctamente.</response>
        /// <response code="400">No se puede actualizar el médico. Verifica que la especialidad exista, los datos sean correctos, o que el ID exista.</response>
        [HttpPut("{id:int}")]
        public ActionResult<Medico> Actualizar(int id, [FromBody] Medico medico)
        {
            var actualizado = _service.Actualizar(id, medico);

            if (actualizado is null)
                return BadRequest("No se puede actualizar el médico. Verifica que la especialidad exista y que los datos sean correctos, o que el ID exista.");

            return Ok(actualizado);
        }

        /// <summary>
        /// Obtiene un médico por su ID.
        /// </summary>
        /// <param name="id">ID del médico a consultar.</param>
        /// <response code="200">Médico encontrado y retornado.</response>
        /// <response code="404">No se encontró el médico con el ID proporcionado.</response>
        [HttpGet("{id:int}")]
        public ActionResult<Medico> ObtenerPorId(int id)
        {
            var medico = _service.ObtenerPorId(id);

            if (medico is null)
                return NotFound();

            return Ok(medico);
        }

        /// <summary>
        /// Obtiene todos los médicos filtrando por especialidad.
        /// </summary>
        /// <param name="especialidadId">ID de la especialidad a filtrar.</param>
        /// <returns>Lista de médicos que pertenecen a la especialidad indicada.</returns>
        /// <response code="200">Lista de médicos filtrada (puede estar vacía si no hay coincidencias).</response>
        [HttpGet("especialidad/{especialidadId:int}")]
        public ActionResult<List<Medico>> ObtenerPorEspecialidad(int especialidadId)
        {
            var medicos = _service.ObtenerPorEspecialidad(especialidadId);

            return Ok(medicos); 
        }

        /// <summary>
        /// Obtiene todos los médicos registrados.
        /// </summary>
        /// <returns>Lista completa de médicos en memoria.</returns>
        /// <response code="200">Lista de médicos (puede estar vacía si no hay registros).</response>
        [HttpGet]
        public ActionResult<List<Medico>> ObtenerTodos()
        {
            return Ok(_service.ObtenerTodos());
        }
    }
}