using DiarioSenac.Data;
using DiarioSenac.Model;

namespace DiarioSenac.Service;

public class UsuarioService
{
    private readonly UsuarioDAO _usuarioDAO = new UsuarioDAO();

    /// <summary>
    /// Cadastra um novo usuário com validações básicas
    /// </summary>
    public void CadastrarUsuario(string nome, string senha)
    {
        ValidarDadosUsuario(nome, senha);

        Usuario usuario = new Usuario
        {
            Nome = nome,
            Senha = senha
        };

        _usuarioDAO.InserirUsuario(usuario);
    }

    /// <summary>
    /// Lista todos os usuários
    /// </summary>
    public List<Usuario> ListarTodosUsuarios()
    {
        return _usuarioDAO.ListarUsuarios();
    }

    /// <summary>
    /// Busca um usuário por ID
    /// </summary>
    public Usuario? BuscarUsuarioPorId(int id)
    {
        return _usuarioDAO.BuscarUsuarioPorId(id);
    }

    /// <summary>
    /// Atualiza dados de um usuário
    /// </summary>
    public void AtualizarUsuario(int id, string? novoNome, string? novaSenha)
    {
        Usuario? usuario = _usuarioDAO.BuscarUsuarioPorId(id);

        if (usuario == null)
            throw new ArgumentException("Usuário não encontrado.");

        if (!string.IsNullOrWhiteSpace(novoNome))
        {
            ValidarNome(novoNome);
            usuario.Nome = novoNome;
        }

        if (!string.IsNullOrWhiteSpace(novaSenha))
        {
            ValidarSenha(novaSenha);
            usuario.Senha = novaSenha;
        }

        _usuarioDAO.AtualizarUsuario(usuario);
    }

    /// <summary>
    /// Remove um usuário pelo ID
    /// </summary>
    public void RemoverUsuario(int id)
    {
        Usuario? usuario = _usuarioDAO.BuscarUsuarioPorId(id);

        if (usuario == null)
            throw new ArgumentException("Usuário não encontrado.");

        _usuarioDAO.RemoverUsuario(id);
    }

    /// <summary>
    /// Valida a senha do usuário
    /// </summary>
    public bool ValidarSenhaUsuario(Usuario usuario, string senhaDigitada)
    {
        return senhaDigitada == usuario.Senha;
    }

    /// <summary>
    /// Validações internas
    /// </summary>
    private void ValidarDadosUsuario(string nome, string senha)
    {
        ValidarNome(nome);
        ValidarSenha(senha);
    }

    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode estar vazio.");
    }

    private void ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("Senha não pode estar vazia.");

        if (senha.Length < 4)
            throw new ArgumentException("Senha deve ter no mínimo 4 caracteres.");
    }
}
