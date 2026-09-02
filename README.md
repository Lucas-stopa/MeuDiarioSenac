# DiarioSenac

Aplicação de console desenvolvida em C# para gerenciamento de usuários e registros pessoais. O projeto utiliza .NET 8, Entity Framework Core e MySQL.

## Funcionalidades

- Cadastro, listagem, busca, atualização e remoção de usuários
- Cadastro, listagem, busca e remoção de registros
- Associação de registros a usuários
- Validação de senha antes do acesso aos registros
- Persistência dos dados em banco MySQL

## Tecnologias

- C#
- .NET 8
- Entity Framework Core 9
- MySQL
- Pomelo Entity Framework Core MySQL

## Estrutura do projeto

```text
DiarioSenac/
├── DiarioSenac.ConsoleApp/   # Ponto de entrada da aplicação
├── DiarioSenac.Classes/      # Menus e regras de interação no console
├── DiarioSenac.Data/         # Entidades, contexto, DAOs e migrations
└── DiarioSenac.sln           # Solução .NET
```

## Pré-requisitos

- .NET SDK 8.0 ou superior
- MySQL Server em execução
- Banco de dados MySQL acessível localmente

## Configuração do banco de dados

A aplicação está configurada por padrão para usar:

```text
Servidor: 127.0.0.1
Porta: 3306
Banco: DiarioSenac
Usuário: root
Senha: admin
```

Crie o banco de dados no MySQL, caso ele ainda não exista:

```sql
CREATE DATABASE DiarioSenac;
```

Se necessário, ajuste a string de conexão no arquivo `DiarioSenac.Data/DiarioSenacContext.cs` antes de executar a aplicação.

> Para ambientes reais, evite manter credenciais diretamente no código-fonte. Prefira variáveis de ambiente ou arquivos de configuração fora do controle de versão.

## Como executar

Na raiz do repositório, restaure as dependências e compile a solução:

```bash
dotnet restore
dotnet build
```

Aplique as migrations no banco de dados:

```bash
dotnet ef database update --project DiarioSenac.Data --startup-project DiarioSenac.ConsoleApp
```

Caso o comando `dotnet ef` ainda não esteja disponível, instale a ferramenta:

```bash
dotnet tool install --global dotnet-ef
```

Execute a aplicação:

```bash
dotnet run --project DiarioSenac.ConsoleApp
```

## Uso

Ao iniciar, o menu principal oferece:

1. Gerenciamento de usuários
2. Gerenciamento de registros
0. Encerramento da aplicação

Para gerenciar registros, primeiro selecione um usuário e informe a senha correspondente.

## Modelo de dados

- `Usuario`: identifica o usuário, armazena nome e senha e possui uma coleção de registros.
- `Registro`: armazena título, data, conteúdo e o identificador do usuário relacionado.

A relação entre as entidades é de um usuário para muitos registros.

## Observações

- A senha atualmente é armazenada e validada diretamente, sem hash criptográfico.
- O projeto não possui testes automatizados registrados neste repositório.
- As migrations ficam em `DiarioSenac.Data/Migrations`.

## Licença

Este projeto ainda não possui uma licença definida.
