using ClinicaPrivada.Models;
using ClinicaPrivada.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaPrivada.Controllers
{
    /// <summary>
    /// Controlador para gestionar pacientes.
    /// Permite crear, actualizar, eliminar y consultar pacientes.
    /// Todos los datos se almacenan en memoria y la ID es automática.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService _service;
        private readonly EliminacionValidatorService _eliminacionValidator;

        public PacienteController(PacienteService service, EliminacionValidatorService eliminacionValidator)
        {
            _service = service;
            _eliminacionValidator = eliminacionValidator;
        }

        /// <summary>
        /// Crea un nuevo paciente.
        /// </summary>
        /// <param name="paciente">Objeto paciente con datos requeridos.</param>
        /// <returns>El paciente creado con ID asignada.</returns>
        /// <response code="201">Paciente creado exitosamente.</response>
        /// <response code="400">Los datos son inválidos.</response>
        [HttpPost]
        public ActionResult<Paciente> Crear([FromBody] Paciente paciente)
        {
            var creado = _service.Crear(paciente);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        /// <summary>
        /// Elimina un paciente por ID.
        /// Solo se permite si el paciente no tiene citas asociadas.
        /// </summary>
        /// <param name="id">ID del paciente a eliminar.</param>
        /// <response code="204">Paciente eliminado correctamente.</response>
        /// <response code="404">No se encontró el paciente con el ID proporcionado.</response>
        /// <response code="400">No se puede eliminar el paciente porque tiene citas asociadas.</response>
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var paciente = _service.ObtenerPorId(id);
            if (paciente is null)
                return NotFound();

            if (!_eliminacionValidator.PacientePuedeEliminarse(id))
                return BadRequest("No se puede eliminar el paciente porque tiene citas asociadas.");

            _service.Eliminar(id);
            return NoContent();
        }

        /// <summary>
        /// Actualiza los datos de un paciente existente.
        /// </summary>
        /// <param name="id">ID del paciente a actualizar.</param>
        /// <param name="paciente">Objeto paciente con datos actualizados.</param>
        /// <returns>Paciente actualizado.</returns>
        /// <response code="200">Paciente actualizado correctamente.</response>
        /// <response code="404">No se encontró el paciente con el ID proporcionado.</response>
        [HttpPut("{id:int}")]
        public ActionResult<Paciente> Actualizar(int id, [FromBody] Paciente paciente)
        {
            var actualizado = _service.Actualizar(id, paciente);

            if (actualizado is null)
                return NotFound();

            return Ok(actualizado);
        }

        /// <summary>
        /// Obtiene un paciente por su ID.
        /// </summary>
        /// <param name="id">ID del paciente a consultar.</param>
        /// <returns>Datos del paciente.</returns>
        /// <response code="200">Paciente encontrado y retornado.</response>
        /// <response code="404">No se encontró el paciente con el ID proporcionado.</response>
        [HttpGet("{id:int}")]
        public ActionResult<Paciente> ObtenerPorId(int id)
        {
            var paciente = _service.ObtenerPorId(id);

            if (paciente is null)
                return NotFound();

            return Ok(paciente);
        }

        /// <summary>
        /// Obtiene todos los pacientes filtrando por sexo.
        /// </summary>
        /// <param name="sexo">Sexo a filtrar (ejemplo: "Masculino", "Femenino").</param>
        /// <returns>Lista de pacientes que coinciden con el sexo.</returns>
        /// <response code="200">Lista de pacientes filtrada (puede estar vacía si no hay coincidencias).</response>
        [HttpGet("sexo/{sexo}")]
        public ActionResult<List<Paciente>> ObtenerPorSexo(Sexo sexo)
        {
            return Ok(_service.ObtenerPorSexo(sexo));
        }

        /// <summary>
        /// Obtiene todos los pacientes registrados.
        /// </summary>
        /// <returns>Lista completa de pacientes en memoria.</returns>
        /// <response code="200">Lista de pacientes (puede estar vacía si no hay registros).</response>
        [HttpGet]
        public ActionResult<List<Paciente>> ObtenerTodos()
        {
            return Ok(_service.ObtenerTodos());
        }
    }
}