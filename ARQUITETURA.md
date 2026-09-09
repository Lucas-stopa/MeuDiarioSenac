# 📋 Arquitetura do Diário Senac

## Visão Geral
O projeto segue uma arquitetura em **camadas** com separação clara de responsabilidades:

```
ConsoleApp (Apresentação)
    ↓
Service (Lógica de Negócio)
    ↓
Data (Acesso aos Dados)
    ↓
Database
```

---

## 🏗️ Estrutura de Projetos

### 1. **DiarioSenac.ConsoleApp** (Entrada)
- Ponto de entrada da aplicação
- Arquivo: `Program.cs`
- Responsabilidade: Iniciar o menu principal

### 2. **DiarioSenac.Classes** (Apresentação/Menu)
- Arquivo: `MenuPrincipal.cs`
- Responsabilidade: Interface com usuário
- **Chama**: Services (RegistroService, UsuarioService)

### 3. **DiarioSenac.Service** (Lógica de Negócio) ✨ NOVO
- Arquivos:
  - `RegistroService.cs` - Gerencia registros
  - `UsuarioService.cs` - Gerencia usuários
- Responsabilidade: 
  - Orquestrar fluxos de negócio
  - Validações em tempo real
  - Chamar camada de dados
- **Chama**: Business (validações) + Data (DAO)

### 4. **DiarioSenac.Business** (Validações)
- Arquivo: `RegistroBusiness.cs`
- Responsabilidade: Regras de validação
- Métodos:
  - `ValidarTitulo(string)` - Valida título em tempo real
  - `ValidarConteudo(string)` - Valida conteúdo em tempo real
  - `Validar(Registro)` - Validação completa

### 5. **DiarioSenac.Data** (Persistência)
- Arquivos:
  - `RegistroDAO.cs` - CRUD de Registros
  - `UsuarioDAO.cs` - CRUD de Usuários
  - `DiarioSenacContext.cs` - Contexto EF Core
  - `Conexao.cs` - Configuração de conexão
- Responsabilidade: Acesso ao banco de dados

### 6. **DiarioSenac.Model** (Entidades)
- Arquivos:
  - `Registro.cs`
  - `Usuario.cs`
- Responsabilidade: Representação das entidades

---

## 🔄 Fluxo de Dados

### Cadastrar Registro (Exemplo)

```
MenuPrincipal.CadastrarRegistro()
    ↓
Validação 1: ValidarTituloEmTempoReal() [RegistroService]
    ↓
Validação 2: ValidarConteudoEmTempoReal() [RegistroService]
    ↓
RegistroService.CadastrarRegistro()
    ↓
RegistroBusiness.Validar() [Validação final]
    ↓
RegistroDAO.InserirRegistro() [Persistência]
    ↓
Database
```

### Validação em Tempo Real

```
Console → Usuário digita título
    ↓
RegistroService.ValidarTituloEmTempoReal()
    ↓
RegistroBusiness.ValidarTitulo()
    ↓
[Se inválido] → ❌ Mensagem de erro em vermelho → Solicitar novamente
[Se válido] → ✓ Prosseguir para próximo campo
```

---

## 📝 Responsabilidades por Camada

| Camada | O que faz | O que NÃO faz |
|--------|-----------|---------------|
| **ConsoleApp** | Inicia app | Lógica de negócio |
| **Classes/Menu** | Interface com usuário | Lógica de negócio |
| **Service** | Orquestra fluxos, valida | SQL direto |
| **Business** | Validações de regras | Acesso a dados |
| **Data** | Acesso ao banco | Validações de negócio |
| **Model** | Estrutura de dados | Lógica ou persistência |

---

## ✨ Benefícios da Arquitetura

✅ **Separação de responsabilidades** - Cada camada tem seu propósito
✅ **Reutilização** - Services podem ser usados por outras interfaces (API, GUI, etc)
✅ **Testabilidade** - Cada camada pode ser testada independentemente
✅ **Manutenibilidade** - Mudanças em uma camada não afetam outras
✅ **Escalabilidade** - Fácil adicionar novos features
✅ **Validação em tempo real** - Feedback imediato ao usuário

---

## 🔧 Como Adicionar Novo Feature

1. **Criar método em Service** (ex: `RegistroService.AtualizarRegistro()`)
2. **Chamar do MenuPrincipal** (ex: `_registroService.AtualizarRegistro()`)
3. **Adicionar validações em Business** (se necessário)
4. **Implementar em DAO** (se necessário acessar dados)

Exemplo:
```csharp
// 1. No RegistroService
public void AtualizarRegistro(int id, string novoTitulo)
{
    ValidarTituloEmTempoReal(novoTitulo);
    _registroDAO.AtualizarRegistro(id, novoTitulo);
}

// 2. No MenuPrincipal
_registroService.AtualizarRegistro(id, titulo);
```

---

## 📦 Dependências entre Projetos

```
ConsoleApp
    └── Classes
        └── Service
            ├── Business
            ├── Data
            └── Model
```

**DiarioSenac.Service** referencia:
- DiarioSenac.Business ✓
- DiarioSenac.Data ✓
- DiarioSenac.Model ✓

---

## 🚀 Build e Execução

```bash
# Build
dotnet build

# Executar
dotnet run --project DiarioSenac.ConsoleApp
```

---

## 📋 Checklist de Boas Práticas

- [ ] Modificar apenas MenuPrincipal para UI
- [ ] Adicionar lógica sempre em Service
- [ ] Validações em Business
- [ ] Acesso a dados em DAO
- [ ] Testes unitários em Service

---

**Versão**: 1.0  
**Data**: 2026-09-09  
**Arquitetor**: IA Assistant
