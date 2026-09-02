namespace DiarioSenac.Data;

public class Registro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime Data { get; set; } = DateTime.Now;
    public string Conteudo { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public required Usuario Usuario { get; set; }
}