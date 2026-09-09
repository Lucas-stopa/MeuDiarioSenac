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
    public void CadastrarRegistro(Usuario usuario, string titulo, string conteudo)
    {
        ValidarTituloEmTempoReal(titulo);
        ValidarConteudoEmTempoReal(conteudo);

        Registro registro = new Registro
        {
            Titulo = titulo,
            Data = DateTime.Now,
            Conteudo = conteudo,
            UsuarioId = usuario.Id,
            Usuario = null!
        };

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
        List<Registro> todosRegistros = _registroDAO.ListarRegistros();
        return todosRegistros.Where(r => r.UsuarioId == usuarioId).ToList();
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
