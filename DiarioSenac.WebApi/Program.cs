using System.Text.Json.Serialization;
using DiarioSenac.Model;
using DiarioSenac.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapGet("/", () => "Boa noite!");

app.MapGet("/motivacional", () => "Lembre-se: você é capaz de alcançar qualquer coisa que se proponha!");

app.MapGet("/registros", () => {
    List<Registro> registros = new RegistroService().ListarTodosRegistros();
    return registros;
    
});

app.Run();
