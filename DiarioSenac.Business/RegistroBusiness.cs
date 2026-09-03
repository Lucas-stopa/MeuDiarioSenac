using DiarioSenac.Model;

namespace DiarioSenac.Business;

public class RegistroBusiness
{
    public void TituloObrigatorio(Registro registro)
    {
        if (string.IsNullOrWhiteSpace(registro.Titulo))
            throw new ArgumentException("O título é obrigatório.");
    }


}
