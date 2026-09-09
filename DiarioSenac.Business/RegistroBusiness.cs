using DiarioSenac.Model;

namespace DiarioSenac.Business;

public class RegistroBusiness
{
    public void TituloObrigatorio(Registro registro)
    {
        if (string.IsNullOrWhiteSpace(registro.Titulo))
            throw new ArgumentException("O título é obrigatório.");
    }

    public void TituloTamanho(Registro registro)
    {
        if (registro.Titulo.Length > 50)
            throw new ArgumentException("O título não pode ter mais de 50 caracteres.");
    }

    public void DataAgora(Registro registro)
    {
        if (registro.Data > DateTime.Now)
            throw new ArgumentException("A data não pode ser futura.");
    }

    public void ConteudoTamanho(Registro registro)
    {
        if (registro.Conteudo.Length > 3000)
            throw new ArgumentException("O conteúdo não pode ter mais de 3000 caracteres.");
    }

    public void ConteudoObrigatorio(Registro registro)
    {
        if (string.IsNullOrWhiteSpace(registro.Conteudo))
            throw new ArgumentException("O conteúdo é obrigatório.");
    }

    public void Validar(Registro registro)
    {
        TituloObrigatorio(registro);
        TituloTamanho(registro);
        DataAgora(registro);
        ConteudoObrigatorio(registro);
        ConteudoTamanho(registro);
    }

    // Métodos para validação individual de campos
    public void ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título é obrigatório.");
        
        if (titulo.Length > 50)
            throw new ArgumentException("O título não pode ter mais de 50 caracteres.");
    }

    public void ValidarConteudo(string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
            throw new ArgumentException("O conteúdo é obrigatório.");
        
        if (conteudo.Length > 3000)
            throw new ArgumentException("O conteúdo não pode ter mais de 3000 caracteres.");
    }
}
