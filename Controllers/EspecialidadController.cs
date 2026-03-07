using ClinicaPrivada.Models;
using ClinicaPrivada.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaPrivada.Controllers
{
    /// <summary>
    /// Controlador para gestionar especialidades médicas.
    /// Permite crear, actualizar, eliminar y consultar especialidades.
    /// La ID se asigna automáticamente.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadController : ControllerBase
    {
        private readonly EspecialidadService _service;
        private readonly EliminacionValidatorService _eliminacionValidator;

        public EspecialidadController(EspecialidadService service, EliminacionValidatorService eliminacionValidator)
        {
            _service = service;
            _eliminacionValidator = eliminacionValidator;
        }

        /// <summary>
        /// Crea una nueva especialidad médica.
        /// </summary>
        /// <param name="especialidad">Objeto Especialidad con nombre y descripción.</param>
        /// <returns>La especialidad creada con ID asignada.</returns>
        /// <response code="201">Especialidad creada correctamente.</response>
        /// <response code="400">La especialidad ya existe o los datos son inválidos.</response>
        [HttpPost]
        public ActionResult<Especialidad> Crear([FromBody] Especialidad especialidad)
        {
            var creado = _service.Crear(especialidad);

            if (creado is null)
                return BadRequest("La especialidad ya existe.");

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        /// <summary>
        /// Elimina una especialidad por ID.
        /// Solo se permite si ningún médico está asociado a esta especialidad.
        /// </summary>
        /// <param name="id">ID de la especialidad a eliminar.</param>
        /// <response code="204">Especialidad eliminada correctamente.</response>
        /// <response code="404">No se encontró la especialidad con el ID proporcionado.</response>
        /// <response code="400">No se puede eliminar la especialidad porque está asociada a uno o más médicos.</response>
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var especialidad = _service.ObtenerPorId(id);
            if (especialidad is null)
                return NotFound();

            if (!_eliminacionValidator.EspecialidadPuedeEliminarse(id))
                return BadRequest("No se puede eliminar la especialidad porque está asociada a uno o más médicos.");

            _service.Eliminar(id);
            return NoContent();
        }

        /// <summary>
        /// Actualiza los datos de una especialidad existente.
        /// </summary>
        /// <param name="id">ID de la especialidad a actualizar.</param>
        /// <param name="especialidad">Objeto Especialidad con datos actualizados.</param>
        /// <response code="200">Especialidad actualizada correctamente.</response>
        /// <response code="404">No se encontró la especialidad con el ID proporcionado.</response>
        [HttpPut("{id:int}")]
        public ActionResult<Especialidad> Actualizar(int id, [FromBody] Especialidad especialidad)
        {
            var actualizado = _service.Actualizar(id, especialidad);

            if (actualizado is null)
                return NotFound();

            return Ok(actualizado);
        }

        /// <summary>
        /// Obtiene una especialidad por su ID.
        /// </summary>
        /// <param name="id">ID de la especialidad a consultar.</param>
        /// <response code="200">Especialidad encontrada y retornada.</response>
        /// <response code="404">No se encontró la especialidad con el ID proporcionado.</response>
        [HttpGet("{id:int}")]
        public ActionResult<Especialidad> ObtenerPorId(int id)
        {
            var especialidad = _service.ObtenerPorId(id);

            if (especialidad is null)
                return NotFound();

            return Ok(especialidad);
        }

        /// <summary>
        /// Obtiene todas las especialidades registradas.
        /// </summary>
        /// <returns>Lista completa de especialidades en memoria.</returns>
        /// <response code="200">Lista de especialidades (puede estar vacía si no hay registros).</response>
        [HttpGet]
        public ActionResult<List<Especialidad>> ObtenerTodos()
        {
            return Ok(_service.ObtenerTodos());
        }
    }
}