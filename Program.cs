using ClinicaPrivada.Services;
using ClinicaPrivada.Middleware;
using System.Text.Json.Serialization;

/// <summary>
/// Punto de entrada de la aplicación ASP.NET Core.
/// Configura los servicios, middleware y endpoints de la API.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------
// Configuración de servicios (Dependency Injection)
// --------------------------------------------------------

/// <summary>
/// Registra servicios singleton para la gestión de entidades de la clínica.
/// Esto asegura que los servicios compartan el mismo estado en memoria durante toda la ejecución.
/// </summary>
builder.Services.AddSingleton<PacienteService>();
builder.Services.AddSingleton<MedicoService>();
builder.Services.AddSingleton<EspecialidadService>();
builder.Services.AddSingleton<ConsultorioService>();
builder.Services.AddSingleton<CitaService>();
builder.Services.AddSingleton<EliminacionValidatorService>();

/// <summary>
/// Configura los controladores y la serialización JSON.
/// Convierte automáticamente los enums a strings en JSON para mejor legibilidad.
/// </summary>
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

/// <summary>
/// Configura Swagger/OpenAPI para la documentación interactiva de la API.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------------------------------------------
// Construcción de la aplicación
// --------------------------------------------------------
var app = builder.Build();

// --------------------------------------------------------
// Middleware global
// --------------------------------------------------------

/// <summary>
/// Middleware global para manejo de errores.
/// Captura excepciones no controladas y devuelve respuestas JSON estandarizadas.
/// </summary>
app.UseErrorHandlingMiddleware();

// --------------------------------------------------------
// Pipeline de la aplicación
// --------------------------------------------------------

// Habilita Swagger solo en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/// <summary>
/// Redirección automática de HTTP a HTTPS.
/// </summary>
app.UseHttpsRedirection();

/// <summary>
/// Middleware de autorización (sin políticas definidas aún).
/// </summary>
app.UseAuthorization();

/// <summary>
/// Mapea los controladores de la API a las rutas correspondientes.
/// </summary>
app.MapControllers();

/// <summary>
/// Ejecuta la aplicación y comienza a escuchar solicitudes HTTP.
/// </summary>
app.Run();