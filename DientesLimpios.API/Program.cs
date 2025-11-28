using DientesLimpios.API.Middlewares;
using DientesLimpios.Aplicacion;
using DientesLimpios.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AgregarServiciosDeAplicacion();
builder.Services.AgregarServiciosDePersistencia(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseManejadorExcepcionesMiddleware();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
