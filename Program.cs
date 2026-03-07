using ClinicaPrivada.Services;
using ClinicaPrivada.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<PacienteService>();
builder.Services.AddSingleton<MedicoService>();
builder.Services.AddSingleton<EspecialidadService>();
builder.Services.AddSingleton<ConsultorioService>();
builder.Services.AddSingleton<CitaService>();
builder.Services.AddSingleton<EliminacionValidatorService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseErrorHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
