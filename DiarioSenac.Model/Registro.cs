using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiarioSenac.Model;

public class Registro
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Titulo { get; set; } = string.Empty;
    
    public DateTime Data { get; set; } = DateTime.Now;
    
    [Required]
    public string Conteudo { get; set; } = string.Empty;
    
    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    
    public Usuario? Usuario { get; set; }
}