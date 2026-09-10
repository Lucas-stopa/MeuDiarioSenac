using DiarioSenac.Data;
using DiarioSenac.Model;

namespace DiarioSenac.Service;

public class UsuarioService
{
    private readonly UsuarioDAO _usuarioDAO = new UsuarioDAO();

    /// <summary>
    /// Cadastra um novo usuário com validações básicas
    /// </summary>
    public void CadastrarUsuario(string nome, string email, string senha)
    {
        ValidarDadosUsuario(nome, email, senha);

        // Verifica se email já existe
        Usuario? usuarioExistente = _usuarioDAO.BuscarUsuarioPorEmail(email);
        if (usuarioExistente != null)
            throw new ArgumentException("Este email já está cadastrado.");

        Usuario usuario = new Usuario
        {
            Nome = nome,
            Email = email,
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
    /// Busca um usuário por Email
    /// </summary>
    public Usuario? BuscarUsuarioPorEmail(string email)
    {
        return _usuarioDAO.BuscarUsuarioPorEmail(email);
    }

    /// <summary>
    /// Autentica um usuário por email e senha
    /// </summary>
    public Usuario? Autenticar(string email, string senha)
    {
        Usuario? usuario = _usuarioDAO.BuscarUsuarioPorEmail(email);

        if (usuario == null)
            return null;

        if (ValidarSenhaUsuario(usuario, senha))
            return usuario;

        return null;
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
    private void ValidarDadosUsuario(string nome, string email, string senha)
    {
        ValidarNome(nome);
        ValidarEmail(email);
        ValidarSenha(senha);
    }

    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode estar vazio.");
    }

    private void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode estar vazio.");

        // Validação básica de email
        if (!email.Contains("@") || !email.Contains("."))
            throw new ArgumentException("Email inválido.");
    }

    private void ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("Senha não pode estar vazia.");

        if (senha.Length < 4)
            throw new ArgumentException("Senha deve ter no mínimo 4 caracteres.");
    }
}
