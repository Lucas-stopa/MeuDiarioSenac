using DiarioSenac.Service;
using DiarioSenac.Model;

namespace DiarioSenac.Classes;

public class MenuPrincipal
{
    private readonly UsuarioService _usuarioService = new UsuarioService();
    private readonly RegistroService _registroService = new RegistroService();
    private Usuario? _usuarioLogado = null;

    public void Menu()
    {
        int escolha;

        do
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║     DIÁRIO SENAC - ACESSO      ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Cadastro");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite a opção desejada: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        RealizarLogin();
                        break;

                    case 2:
                        RealizarCadastro();
                        break;

                    case 0:
                        Console.WriteLine("Encerrando aplicação...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Digite uma opção válida.");
                Console.ReadLine();
                escolha = -1;
            }

        } while (escolha != 0);
    }

    public void RealizarLogin()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║         TELA DE LOGIN          ║");
        Console.WriteLine("╚════════════════════════════════╝");

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.Write("Senha: ");
        string senha = Console.ReadLine() ?? string.Empty;

        try
        {
            Usuario? usuarioAutenticado = _usuarioService.Autenticar(email, senha);

            if (usuarioAutenticado == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Email ou senha incorretos.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            _usuarioLogado = usuarioAutenticado;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ Bem-vindo, {usuarioAutenticado.Nome}!");
            Console.ResetColor();
            Console.ReadLine();

            MenuPrincipalLogado(usuarioAutenticado);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Erro: {ex.Message}");
            Console.ResetColor();
            Console.ReadLine();
        }
    }

    public void RealizarCadastro()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║      CADASTRO DE NOVO USUÁRIO  ║");
        Console.WriteLine("╚════════════════════════════════╝");

        Console.Write("Nome completo: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.Write("Senha (mín. 4 caracteres): ");
        string senha = Console.ReadLine() ?? string.Empty;

        try
        {
            _usuarioService.CadastrarUsuario(nome, email, senha);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✓ Usuário cadastrado com sucesso!");
            Console.WriteLine("Você já pode fazer login com seus dados.");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ {ex.Message}");
            Console.ResetColor();
        }

        Console.ReadLine();
    }

    public void MenuPrincipalLogado(Usuario usuarioLogado)
    {
        int escolha;

        do
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine($"║ Bem-vindo, {usuarioLogado.Nome,-18}║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine("1 - Gerenciar Registros");
            Console.WriteLine("2 - Meu Perfil");
            Console.WriteLine("0 - Logout");
            Console.Write("Digite a opção desejada: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        MenuRegistros(usuarioLogado);
                        break;

                    case 2:
                        MenuMeuPerfil(usuarioLogado);
                        break;

                    case 0:
                        _usuarioLogado = null;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Você foi desconectado.");
                        Console.ResetColor();
                        Console.ReadLine();
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Digite uma opção válida.");
                Console.ReadLine();
                escolha = -1;
            }

        } while (escolha != 0);
    }

    public void MenuMeuPerfil(Usuario usuarioLogado)
    {
        int escolha;

        do
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║        MEU PERFIL              ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine($"Nome: {usuarioLogado.Nome}");
            Console.WriteLine($"Email: {usuarioLogado.Email}");
            Console.WriteLine($"Total de Registros: {usuarioLogado.Registros.Count}");
            Console.WriteLine("\nO que deseja fazer?");
            Console.WriteLine("1 - Alterar Nome");
            Console.WriteLine("2 - Alterar Senha");
            Console.WriteLine("3 - Alterar Nome e Senha");
            Console.WriteLine("0 - Voltar");
            Console.Write("Digite a opção desejada: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        AlterarNomeUsuario(usuarioLogado);
                        break;

                    case 2:
                        AlterarSenhaUsuario(usuarioLogado);
                        break;

                    case 3:
                        AlterarNomeUsuario(usuarioLogado);
                        AlterarSenhaUsuario(usuarioLogado);
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Digite uma opção válida.");
                Console.ReadLine();
                escolha = -1;
            }

        } while (escolha != 0);
    }

    private void AlterarNomeUsuario(Usuario usuarioLogado)
    {
        Console.Write("Novo nome: ");
        string novoNome = Console.ReadLine() ?? string.Empty;

        try
        {
            _usuarioService.AtualizarUsuario(usuarioLogado.Id, novoNome, null);
            usuarioLogado.Nome = novoNome;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Nome atualizado com sucesso!");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ {ex.Message}");
            Console.ResetColor();
        }

        Console.ReadLine();
    }

    private void AlterarSenhaUsuario(Usuario usuarioLogado)
    {
        Console.Write("Nova senha: ");
        string novaSenha = Console.ReadLine() ?? string.Empty;

        try
        {
            _usuarioService.AtualizarUsuario(usuarioLogado.Id, null, novaSenha);
            usuarioLogado.Senha = novaSenha;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Senha atualizada com sucesso!");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ {ex.Message}");
            Console.ResetColor();
        }

        Console.ReadLine();
    }


    public void MenuRegistros(Usuario usuarioLogado)
    {
        int escolha;

        do
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║    GERENCIAR REGISTROS         ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine("1 - Cadastrar novo registro");
            Console.WriteLine("2 - Listar meus registros");
            Console.WriteLine("3 - Buscar registro por ID");
            Console.WriteLine("4 - Remover registro");
            Console.WriteLine("0 - Voltar");
            Console.Write("Digite a opção desejada: ");

            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        CadastrarRegistro(usuarioLogado);
                        break;

                    case 2:
                        ListarRegistros(usuarioLogado);
                        break;

                    case 3:
                        BuscarRegistroPorId(usuarioLogado);
                        break;

                    case 4:
                        RemoverRegistroPorId(usuarioLogado);
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Digite uma opção válida.");
                Console.ReadLine();
                escolha = -1;
            }

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
                _registroService.ValidarTituloEmTempoReal(titulo);
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
                _registroService.ValidarConteudoEmTempoReal(conteudo);
                conteudoValido = true;
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ {ex.Message}");
                Console.ResetColor();
            }
        }

        try
        {
            _registroService.CadastrarRegistro(usuario, titulo, conteudo);
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
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Erro ao cadastrar registro: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPressione Enter para voltar...");
        Console.ReadLine();
    }

    public void ListarRegistros(Usuario usuario)
    {
        try
        {
            List<Registro> registrosUsuario = _registroService.ListarRegistrosDoUsuario(usuario.Id);

            if (registrosUsuario.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Nenhum registro encontrado para {usuario.Nome}.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Registros de {usuario.Nome}:");
                Console.ResetColor();

                foreach (Registro registro in registrosUsuario)
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine($"ID: {registro.Id}");
                    Console.WriteLine($"Título: {registro.Titulo}");
                    Console.WriteLine($"Data: {registro.Data:yyyy-MM-dd HH:mm:ss}");
                    Console.WriteLine($"Conteúdo: {registro.Conteudo}");
                }

                Console.WriteLine("------------------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Erro ao listar registros: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPressione Enter para voltar...");
        Console.ReadLine();
    }

    public void BuscarRegistroPorId(Usuario usuario)
    {
        Console.Write("Informe o ID do registro: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ID inválido.");
            Console.ResetColor();
            Console.WriteLine("\nPressione Enter para voltar...");
            Console.ReadLine();
            return;
        }

        try
        {
            Registro? registro = _registroService.BuscarRegistroPorId(id, usuario.Id);

            if (registro == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Registro não encontrado ou não pertence a este usuário.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine($"ID: {registro.Id}");
                Console.WriteLine($"Título: {registro.Titulo}");
                Console.WriteLine($"Data: {registro.Data:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Conteúdo: {registro.Conteudo}");
                Console.WriteLine("------------------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Erro ao buscar registro: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPressione Enter para voltar...");
        Console.ReadLine();
    }

    public void RemoverRegistroPorId(Usuario usuario)
    {
        Console.Write("Informe o ID do registro: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ID inválido.");
            Console.ResetColor();
            Console.WriteLine("\nPressione Enter para voltar...");
            Console.ReadLine();
            return;
        }

        try
        {
            if (_registroService.RemoverRegistro(id, usuario.Id))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Registro removido com sucesso!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Registro não encontrado ou não pertence a este usuário.");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Erro ao remover registro: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\nPressione Enter para voltar...");
        Console.ReadLine();
    }
}