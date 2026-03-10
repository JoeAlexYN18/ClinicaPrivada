using System.Net;
using System.Text.Json;

namespace ClinicaPrivada.Middleware
{
    /// <summary>
    /// Middleware encargado de manejar excepciones globales de la aplicación.
    /// Captura errores no controlados durante el procesamiento de una solicitud HTTP,
    /// los registra en el sistema de logging y devuelve una respuesta JSON estandarizada.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Inicializa una nueva instancia del middleware de manejo de errores.
        /// </summary>
        /// <param name="next">
        /// Delegado que representa el siguiente middleware en el pipeline de la aplicación.
        /// </param>
        /// <param name="logger">
        /// Servicio de logging utilizado para registrar excepciones ocurridas durante la ejecución.
        /// </param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Procesa la solicitud HTTP entrante y captura cualquier excepción no controlada.
        /// </summary>
        /// <param name="context">
        /// Contexto de la solicitud HTTP actual.
        /// </param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica del middleware.
        /// </returns>
        /// <remarks>
        /// Si ocurre una excepción durante la ejecución de los middlewares posteriores,
        /// esta será registrada y se devolverá una respuesta HTTP 500 con un mensaje JSON.
        /// </remarks>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error inesperado");
                await HandleExceptionAsync(context);
            }
        }

        /// <summary>
        /// Construye y envía la respuesta HTTP cuando ocurre una excepción no controlada.
        /// </summary>
        /// <param name="context">Contexto de la solicitud HTTP.</param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica de escritura de la respuesta.
        /// </returns>
        /// <remarks>
        /// Devuelve una respuesta JSON con código de estado 500 (Internal Server Error).
        /// </remarks>
        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                Mensaje = "Ocurrió un error inesperado. Intente nuevamente más tarde."
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }

    /// <summary>
    /// Clase de extensión que facilita el registro del middleware
    /// <see cref="ErrorHandlingMiddleware"/> dentro del pipeline de la aplicación.
    /// </summary>
    public static class ErrorHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Agrega el middleware de manejo global de errores al pipeline de la aplicación.
        /// </summary>
        /// <param name="builder">Instancia del constructor de la aplicación.</param>
        /// <returns>
        /// La misma instancia de <see cref="IApplicationBuilder"/> para permitir encadenamiento.
        /// </returns>
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}