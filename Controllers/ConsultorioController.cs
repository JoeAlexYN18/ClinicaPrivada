using ClinicaPrivada.Models;
using ClinicaPrivada.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaPrivada.Controllers
{
    /// <summary>
    /// Controlador para gestionar consultorios médicos.
    /// Permite crear, actualizar, eliminar y consultar consultorios.
    /// La ID se asigna automáticamente.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultorioController : ControllerBase
    {
        private readonly ConsultorioService _service;
        private readonly EliminacionValidatorService _eliminacionValidator;

        public ConsultorioController(ConsultorioService service, EliminacionValidatorService eliminacionValidator)
        {
            _service = service;
            _eliminacionValidator = eliminacionValidator;
        }

        /// <summary>
        /// Crea un nuevo consultorio.
        /// </summary>
        /// <param name="consultorio">Objeto Consultorio con nombre y ubicación.</param>
        /// <returns>El consultorio creado con ID asignada.</returns>
        /// <response code="201">Consultorio creado correctamente.</response>
        /// <response code="400">El consultorio ya existe o los datos son inválidos.</response>
        [HttpPost]
        public ActionResult<Consultorio> Crear([FromBody] Consultorio consultorio)
        {
            var creado = _service.Crear(consultorio);

            if (creado is null)
                return BadRequest("El consultorio ya existe.");

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        /// <summary>
        /// Elimina un consultorio por ID.
        /// Solo se permite si el consultorio no tiene citas asociadas.
        /// </summary>
        /// <param name="id">ID del consultorio a eliminar.</param>
        /// <response code="204">Consultorio eliminado correctamente.</response>
        /// <response code="404">No se encontró el consultorio con el ID proporcionado.</response>
        /// <response code="400">No se puede eliminar el consultorio porque tiene citas asociadas.</response>
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var consultorio = _service.ObtenerPorId(id);
            if (consultorio is null)
                return NotFound();

            if (!_eliminacionValidator.ConsultorioPuedeEliminarse(id))
                return BadRequest("No se puede eliminar el consultorio porque tiene citas asociadas.");

            _service.Eliminar(id);
            return NoContent();
        }

        /// <summary>
        /// Actualiza los datos de un consultorio existente.
        /// </summary>
        /// <param name="id">ID del consultorio a actualizar.</param>
        /// <param name="consultorio">Objeto Consultorio con datos actualizados.</param>
        /// <response code="200">Consultorio actualizado correctamente.</response>
        /// <response code="404">No se encontró el consultorio con el ID proporcionado.</response>
        [HttpPut("{id:int}")]
        public ActionResult<Consultorio> Actualizar(int id, [FromBody] Consultorio consultorio)
        {
            var actualizado = _service.Actualizar(id, consultorio);

            if (actualizado is null)
                return NotFound();

            return Ok(actualizado);
        }

        /// <summary>
        /// Obtiene un consultorio por su ID.
        /// </summary>
        /// <param name="id">ID del consultorio a consultar.</param>
        /// <response code="200">Consultorio encontrado y retornado.</response>
        /// <response code="404">No se encontró el consultorio con el ID proporcionado.</response>
        [HttpGet("{id:int}")]
        public ActionResult<Consultorio> ObtenerPorId(int id)
        {
            var consultorio = _service.ObtenerPorId(id);

            if (consultorio is null)
                return NotFound();

            return Ok(consultorio);
        }

        /// <summary>
        /// Obtiene todos los consultorios registrados.
        /// </summary>
        /// <returns>Lista completa de consultorios en memoria.</returns>
        /// <response code="200">Lista de consultorios (puede estar vacía si no hay registros).</response>
        [HttpGet]
        public ActionResult<List<Consultorio>> ObtenerTodos()
        {
            return Ok(_service.ObtenerTodos());
        }
    }
}