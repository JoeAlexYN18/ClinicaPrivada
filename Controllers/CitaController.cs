using ClinicaPrivada.Models;
using ClinicaPrivada.DTOs;
using ClinicaPrivada.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaPrivada.Controllers
{
    /// <summary>
    /// Controlador para gestionar citas médicas.
    /// Permite crear, actualizar, eliminar y consultar citas.
    /// Las IDs de Cita se asignan automáticamente y la FechaCreacion se genera automáticamente.
    /// Al crear o actualizar, se valida que PacienteId, MedicoId y ConsultorioId existan.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CitaController : ControllerBase
    {
        private readonly CitaService _service;

        public CitaController(CitaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Crea una nueva cita médica.
        /// </summary>
        /// <param name="cita">Objeto Cita con PacienteId, MedicoId, ConsultorioId, Motivo y Estado.</param>
        /// <returns>DTO de la cita creada con información completa de paciente, médico y consultorio.</returns>
        /// <response code="201">Cita creada correctamente.</response>
        /// <response code="400">No se puede crear la cita. Verifica que Paciente, Medico y Consultorio existan y que los datos sean correctos.</response>
        [HttpPost]
        public ActionResult<CitaDTO> Crear([FromBody] Cita cita)
        {
            var creado = _service.Crear(cita);

            if (creado is null)
                return BadRequest("No se puede crear la cita. Verifica que Paciente, Medico y Consultorio existan.");

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, _service.ObtenerTodos().First(c => c.Id == creado.Id));
        }

        /// <summary>
        /// Actualiza una cita existente.
        /// </summary>
        /// <param name="id">ID de la cita a actualizar.</param>
        /// <param name="cita">Objeto Cita con los datos actualizados.</param>
        /// <returns>DTO de la cita actualizada.</returns>
        /// <response code="200">Cita actualizada correctamente.</response>
        /// <response code="400">No se puede actualizar la cita. Verifica que el ID exista y que Paciente, Medico y Consultorio sean válidos.</response>
        [HttpPut("{id:int}")]
        public ActionResult<CitaDTO> Actualizar(int id, [FromBody] Cita cita)
        {
            var actualizado = _service.Actualizar(id, cita);

            if (actualizado is null)
                return BadRequest("No se puede actualizar la cita. Verifica que el ID exista y que Paciente, Medico y Consultorio sean válidos.");

            return Ok(_service.ObtenerTodos().First(c => c.Id == actualizado.Id));
        }

        /// <summary>
        /// Elimina una cita por ID.
        /// </summary>
        /// <param name="id">ID de la cita a eliminar.</param>
        /// <response code="204">Cita eliminada correctamente.</response>
        /// <response code="404">No se encontró la cita con el ID proporcionado.</response>
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            if (!_service.Eliminar(id))
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Obtiene una cita por su ID.
        /// </summary>
        /// <param name="id">ID de la cita a consultar.</param>
        /// <returns>DTO de la cita con información completa de paciente, médico y consultorio.</returns>
        /// <response code="200">Cita encontrada y retornada.</response>
        /// <response code="404">No se encontró la cita con el ID proporcionado.</response>
        [HttpGet("{id:int}")]
        public ActionResult<CitaDTO> ObtenerPorId(int id)
        {
            var cita = _service.ObtenerPorId(id);
            if (cita is null)
                return NotFound();

            return Ok(_service.ObtenerTodos().First(c => c.Id == id));
        }

        /// <summary>
        /// Obtiene todas las citas registradas.
        /// </summary>
        /// <returns>Lista de DTOs de citas con información completa de paciente, médico y consultorio.</returns>
        /// <response code="200">Lista de citas (puede estar vacía si no hay registros).</response>
        [HttpGet]
        public ActionResult<List<CitaDTO>> ObtenerTodos()
        {
            return Ok(_service.ObtenerTodos());
        }
    }
}