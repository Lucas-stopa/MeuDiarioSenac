using DiarioSenac.Data;
using DiarioSenac.Business;
using DiarioSenac.Model;

namespace DiarioSenac.Classes;

public class MenuPrincipal
{
    public Conexao bdconexao = new Conexao();
    public UsuarioDAO usuarioDAO = new UsuarioDAO();
    private readonly RegistroBusiness registroBusiness = new RegistroBusiness();

    public void Menu()
    {
        int escolha;

        do
        {
            Console.WriteLine("===== DIÁRIO SENAC =====");
            Console.WriteLine("1 - Gerenciar Usuários");
            Console.WriteLine("2 - Gerenciar Registros");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite a opção desejada: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        MenuUsuarios();
                        break;

                    case 2:
                        MenuRegistros();
                        break;

                    case 0:
                        Console.WriteLine("Encerrando aplicação...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Digite uma opção válida.");
                escolha = -1;
            }

            Console.WriteLine();

        } while (escolha != 0);
    }


    public void MenuUsuarios()
    {
        int escolha;

        do
        {
            Console.WriteLine("===== GERENCIAR USUÁRIOS =====");
            Console.WriteLine("1 - Cadastrar novo usuário");
            Console.WriteLine("2 - Listar todos os usuários");
            Console.WriteLine("3 - Buscar usuário por ID");
            Console.WriteLine("4 - Atualizar usuário");
            Console.WriteLine("5 - Remover usuário");
            Console.WriteLine("0 - Voltar ao menu anterior");
            Console.Write("Digite a opção desejada: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        CadastrarUsuario();
                        break;

                    case 2:
                        ListarUsuarios();
                        break;

                    case 3:
                        BuscarUsuarioPorId();
                        break;

                    case 4:
                        AtualizarUsuario();
                        break;

                    case 5:
                        RemoverUsuarioPorId();
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Digite uma opção válida.");
                escolha = -1;
            }

            Console.WriteLine();

        } while (escolha != 0);
    }

    public void CadastrarUsuario()
    {
        Console.Write("Nome do usuário: ");
        string nome = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome não pode estar vazio.");
            return;
        }

        Console.Write("Senha: ");
        string senha = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(senha))
        {
            Console.WriteLine("Senha não pode estar vazia.");
            return;
        }

        if (senha.Length < 4)
        {
            Console.WriteLine("Senha deve ter no mínimo 4 caracteres.");
            return;
        }

        Usuario usuario = new Usuario
        {
            Nome = nome,
            Senha = senha
        };

        usuarioDAO.InserirUsuario(usuario);
        Console.WriteLine("Usuário cadastrado com sucesso!");
    }

    public void ListarUsuarios()
    {
        List<Usuario> usuarios = usuarioDAO.ListarUsuarios();

        if (usuarios.Count == 0)
        {
            Console.WriteLine("Nenhum usuário encontrado.");
            return;
        }

        foreach (Usuario usuario in usuarios)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"ID: {usuario.Id}");
            Console.WriteLine($"Nome: {usuario.Nome}");
            Console.WriteLine($"Total de Registros: {usuario.Registros.Count}");
        }

        Console.WriteLine("------------------------------------");
    }

    public void BuscarUsuarioPorId()
    {
        Console.Write("Informe o ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Usuario? usuario = usuarioDAO.BuscarUsuarioPorId(id);

        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        Console.WriteLine("------------------------------------");
        Console.WriteLine($"ID: {usuario.Id}");
        Console.WriteLine($"Nome: {usuario.Nome}");
        Console.WriteLine($"Total de Registros: {usuario.Registros.Count}");
        Console.WriteLine("------------------------------------");
    }

    public void AtualizarUsuario()
    {
        Console.Write("Informe o ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Usuario? usuario = usuarioDAO.BuscarUsuarioPorId(id);

        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        Console.WriteLine("\nO que deseja atualizar?");
        Console.WriteLine("1 - Nome");
        Console.WriteLine("2 - Senha");
        Console.WriteLine("3 - Nome e Senha");
        Console.Write("Digite a opção: ");

        if (!int.TryParse(Console.ReadLine(), out int opcao))
        {
            Console.WriteLine("Opção inválida.");
            return;
        }

        bool atualizou = false;

        if (opcao == 1 || opcao == 3)
        {
            Console.Write("Novo nome do usuário: ");
            string novoNome = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(novoNome))
            {
                Console.WriteLine("Nome não pode estar vazio.");
                return;
            }

            usuario.Nome = novoNome;
            atualizou = true;
        }

        if (opcao == 2 || opcao == 3)
        {
            Console.Write("Nova senha: ");
            string novaSenha = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(novaSenha))
            {
                Console.WriteLine("Senha não pode estar vazia.");
                return;
            }

            if (novaSenha.Length < 4)
            {
                Console.WriteLine("Senha deve ter no mínimo 4 caracteres.");
                return;
            }

            usuario.Senha = novaSenha;
            atualizou = true;
        }

        if (!atualizou)
        {
            Console.WriteLine("Nenhum campo foi selecionado para atualização.");
            return;
        }

        usuarioDAO.AtualizarUsuario(usuario);
        Console.WriteLine("Usuário atualizado com sucesso!");
    }

    public void RemoverUsuarioPorId()
    {
        Console.Write("Informe o ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Usuario? usuario = usuarioDAO.BuscarUsuarioPorId(id);

        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        usuarioDAO.RemoverUsuario(id);
        Console.WriteLine("Usuário removido com sucesso!");
    }


    public void MenuRegistros()
    {
        // Selecionar usuário primeiro
        Console.WriteLine("===== SELECIONAR USUÁRIO =====");
        List<Usuario> usuarios = usuarioDAO.ListarUsuarios();

        if (usuarios.Count == 0)
        {
            Console.WriteLine("Nenhum usuário disponível. Cadastre um usuário primeiro.");
            return;
        }

        Console.WriteLine("Usuários disponíveis:");
        foreach (Usuario u in usuarios)
        {
            Console.WriteLine($"ID: {u.Id} - Nome: {u.Nome}");
        }

        Console.Write("\nDigite o ID do usuário para gerenciar registros: ");
        if (!int.TryParse(Console.ReadLine(), out int usuarioSelecionadoId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Usuario? usuarioSelecionado = usuarioDAO.BuscarUsuarioPorId(usuarioSelecionadoId);

        if (usuarioSelecionado == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        // Validar senha
        if (!ValidarSenhaUsuario(usuarioSelecionado))
        {
            Console.WriteLine("Acesso negado. Senha incorreta.");
            return;
        }

        Console.WriteLine($"\nVocê está gerenciando registros do usuário: {usuarioSelecionado.Nome}\n");

        int escolha;

        do
        {
            Console.WriteLine("===== GERENCIAR REGISTROS =====");
            Console.WriteLine("1 - Cadastrar novo registro");
            Console.WriteLine("2 - Listar registros deste usuário");
            Console.WriteLine("3 - Buscar registro por ID");
            Console.WriteLine("4 - Remover registro");
            Console.WriteLine("0 - Voltar ao menu anterior");
            Console.Write("Digite a opção desejada: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        CadastrarRegistro(usuarioSelecionado);
                        break;

                    case 2:
                        ListarRegistros(usuarioSelecionado);
                        break;

                    case 3:
                        BuscarRegistroPorId(usuarioSelecionado);
                        break;

                    case 4:
                        RemoverRegistroPorId(usuarioSelecionado);
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Digite uma opção válida.");
                escolha = -1;
            }

            Console.WriteLine();

        } while (escolha != 0);
    }

    public void CadastrarRegistro(Usuario usuario)
    {
        // Validar título
        string titulo = string.Empty;
        bool tituloValido = false;

        while (!tituloValido)
        {
            Console.Write("Título: ");
            titulo = Console.ReadLine() ?? string.Empty;

            try
            {
                registroBusiness.ValidarTitulo(titulo);
                tituloValido = true;
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ {ex.Message}");
                Console.ResetColor();
            }
        }

        // Validar conteúdo
        string conteudo = string.Empty;
        bool conteudoValido = false;

        while (!conteudoValido)
        {
            Console.Write("Conteúdo: ");
            conteudo = Console.ReadLine() ?? string.Empty;

            try
            {
                registroBusiness.ValidarConteudo(conteudo);
                conteudoValido = true;
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ {ex.Message}");
                Console.ResetColor();
            }
        }

        Registro registro = new Registro
        {
            Titulo = titulo,
            Data = DateTime.Now,
            Conteudo = conteudo,
            UsuarioId = usuario.Id,
            Usuario = null!
        };

        try
        {
            registroBusiness.Validar(registro);
            bdconexao.InserirRegistro(registro);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Registro cadastrado com sucesso!");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ {ex.Message}");
            Console.ResetColor();
        }
    }

    public void ListarRegistros(Usuario usuario)
    {
        List<Registro> todosRegistros = bdconexao.ListarRegistros();
        List<Registro> registrosUsuario = todosRegistros.Where(r => r.UsuarioId == usuario.Id).ToList();

        if (registrosUsuario.Count == 0)
        {
            Console.WriteLine($"Nenhum registro encontrado para {usuario.Nome}.");
            return;
        }

        Console.WriteLine($"Registros de {usuario.Nome}:");
        foreach (Registro registro in registrosUsuario)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"ID: {registro.Id}");
            Console.WriteLine($"Título: {registro.Titulo}");
            Console.WriteLine($"Data: {registro.Data:yyyy-MM-dd}");
            Console.WriteLine($"Conteúdo: {registro.Conteudo}");
        }

        Console.WriteLine("------------------------------------");
    }

    public void BuscarRegistroPorId(Usuario usuario)
    {
        Console.Write("Informe o ID do registro: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Registro? registro = bdconexao.BuscarRegistroPorId(id);

        if (registro == null || registro.UsuarioId != usuario.Id)
        {
            Console.WriteLine("Registro não encontrado ou não pertence a este usuário.");
            return;
        }

        Console.WriteLine("------------------------------------");
        Console.WriteLine($"ID: {registro.Id}");
        Console.WriteLine($"Título: {registro.Titulo}");
        Console.WriteLine($"Data: {registro.Data:dd-MM-yyyy}");
        Console.WriteLine($"Conteúdo: {registro.Conteudo}");
        Console.WriteLine("------------------------------------");
    }

    public void RemoverRegistroPorId(Usuario usuario)
    {
        Console.Write("Informe o ID do registro: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Registro? registro = bdconexao.BuscarRegistroPorId(id);

        if (registro == null || registro.UsuarioId != usuario.Id)
        {
            Console.WriteLine("Registro não encontrado ou não pertence a este usuário.");
            return;
        }

        bdconexao.RemoverRegistro(id);

        Console.WriteLine("Registro removido com sucesso!");
    }

    private bool ValidarSenhaUsuario(Usuario usuario)
    {
        Console.Write("Digite a senha para acessar os registros: ");
        string senhaDigitada = Console.ReadLine() ?? string.Empty;

        if (senhaDigitada == usuario.Senha)
        {
            return true;
        }

        return false;
    }
}