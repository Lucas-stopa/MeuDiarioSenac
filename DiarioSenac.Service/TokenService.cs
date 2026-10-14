using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DiarioSenac.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        var jwtSettings = _configuration.GetSection("JWTSettings");
        var chaveSecreta = jwtSettings["SecretKey"] ??
            throw new InvalidOperationException("A chave secreta do JWT não está configurada.");
        var expirationInHours = jwtSettings.GetValue<double>("expirationInHours");

        var chaveSecretaBytes = Encoding.ASCII.GetBytes(chaveSecreta);
        var credenciais = new SigningCredentials(
            new SymmetricSecurityKey(chaveSecretaBytes),
            SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expirationInHours),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = credenciais
        };

        var token = new JwtSecurityTokenHandler().CreateToken(descriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}