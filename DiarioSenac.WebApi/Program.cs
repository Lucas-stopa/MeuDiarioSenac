using System.Text;
using System.Text.Json.Serialization;
using DiarioSenac.Model;
using DiarioSenac.Service;
using DiarioSenac.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetRequiredSection("JWTSettings");
var secretKey = jwtSettings["SecretKey"]
    ?? throw new InvalidOperationException("A chave secreta do JWT não está configurada.");
var issuer = jwtSettings["Issuer"]
    ?? throw new InvalidOperationException("O emissor do JWT não está configurado.");
var audience = jwtSettings["Audience"]
    ?? throw new InvalidOperationException("O público do JWT não está configurado.");
var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);

if (secretKeyBytes.Length < 32)
    throw new InvalidOperationException("A chave secreta do JWT deve possuir pelo menos 32 bytes.");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login/auth", ([FromBody] LoginRequest request) =>
{
    var usuarioService = new UsuarioService();
    var usuario = usuarioService.Autenticar(request.Email, request.Senha);

    if (usuario is null)
        return Results.Unauthorized();

    var tokenService = new TokenService(builder.Configuration);
    var token = tokenService.GerarToken(usuario);

    return Results.Ok(token);
});

var registrosGroup = app.MapGroup("/registros").RequireAuthorization();

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
