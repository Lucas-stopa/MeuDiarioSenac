using System.Text.Json.Serialization;
using DiarioSenac.Model;
using DiarioSenac.Service;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
var registrosGroup = app.MapGroup("/registros");

app.MapGet("/", () => "Boa noite!");

app.MapGet("/motivacional", () => "Lembre-se: você é capaz de alcançar qualquer coisa que se proponha!");

registrosGroup.MapGet("/", () => {
    List<Registro> registros = new RegistroService().ListarTodosRegistros();
    return registros;
});

registrosGroup.MapPost("/", ([FromBody] Registro registro) => {
    new RegistroService().CadastrarRegistro(registro);

    return Results.Created($"/registros/{registro.Id}", registro);
});

registrosGroup.MapPut("/{id}", (int id, [FromBody] Registro registro) =>
{
    try
    {
        new RegistroService().AtualizarRegistro(id, registro.UsuarioId, registro.Titulo, registro.Conteudo);
        return Results.Ok(registro);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

registrosGroup.MapDelete("/{id}", (int id, [FromBody] int usuarioId) =>
{
    try
    {
        new RegistroService().RemoverRegistro(id, usuarioId);
        return Results.Ok();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();
