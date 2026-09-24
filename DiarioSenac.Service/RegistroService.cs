using DiarioSenac.Business;
using DiarioSenac.Data;
using DiarioSenac.Model;

namespace DiarioSenac.Service;

public class RegistroService
{
    private readonly RegistroDAO _registroDAO = new RegistroDAO();
    private readonly RegistroBusiness _registroBusiness = new RegistroBusiness();

    /// <summary>
    /// Cadastra um novo registro com validação em tempo real
    /// </summary>
    public void CadastrarRegistro(Registro registro)
    {
        ValidarTituloEmTempoReal(registro.Titulo);
        ValidarConteudoEmTempoReal(registro.Conteudo);

        registro.Data = DateTime.Now;

        _registroBusiness.Validar(registro);
        _registroDAO.InserirRegistro(registro);
    }

    /// <summary>
    /// Valida o título em tempo real (logo após digitação)
    /// </summary>
    public void ValidarTituloEmTempoReal(string titulo)
    {
        _registroBusiness.ValidarTitulo(titulo);
    }

    /// <summary>
    /// Valida o conteúdo em tempo real (logo após digitação)
    /// </summary>
    public void ValidarConteudoEmTempoReal(string conteudo)
    {
        _registroBusiness.ValidarConteudo(conteudo);
    }

    /// <summary>
    /// Lista todos os registros de um usuário específico
    /// </summary>
    public List<Registro> ListarRegistrosDoUsuario(int usuarioId)
    {
        return _registroDAO.ListarRegistrosPorUsuario(usuarioId);
    }

    /// <summary>
    /// Busca um registro por ID verificando se pertence ao usuário
    /// </summary>
    public Registro? BuscarRegistroPorId(int registroId, int usuarioId)
    {
        Registro? registro = _registroDAO.BuscarRegistroPorId(registroId);

        if (registro == null || registro.UsuarioId != usuarioId)
            return null;

        return registro;
    }

    /// <summary>
    /// Atualiza um registro verificando se pertence ao usuário
    /// </summary>
    public void AtualizarRegistro(int registroId, int usuarioId, string? novoTitulo, string? novoConteudo)
    {
        Registro? registro = _registroDAO.BuscarRegistroPorId(registroId);

        if (registro == null || registro.UsuarioId != usuarioId)
            throw new ArgumentException("Registro não encontrado ou não pertence ao usuário.");

        if (string.IsNullOrWhiteSpace(novoTitulo) && string.IsNullOrWhiteSpace(novoConteudo))
            throw new ArgumentException("Informe pelo menos um campo para atualizar.");

        if (!string.IsNullOrWhiteSpace(novoTitulo))
        {
            ValidarTituloEmTempoReal(novoTitulo);
            registro.Titulo = novoTitulo;
        }

        if (!string.IsNullOrWhiteSpace(novoConteudo))
        {
            ValidarConteudoEmTempoReal(novoConteudo);
            registro.Conteudo = novoConteudo;
        }

        _registroDAO.AtualizarRegistro(registro);
    }

    /// <summary>
    /// Remove um registro verificando se pertence ao usuário
    /// </summary>
    public bool RemoverRegistro(int registroId, int usuarioId)
    {
        Registro? registro = _registroDAO.BuscarRegistroPorId(registroId);

        if (registro == null || registro.UsuarioId != usuarioId)
            return false;

        _registroDAO.RemoverRegistro(registroId);
        return true;
    }

    /// <summary>
    /// Lista todos os registros do sistema
    /// </summary>
    public List<Registro> ListarTodosRegistros()
    {
        return _registroDAO.ListarRegistros();
    }
}
