# Good Hamburger | Solução de Gerenciamento de Pedidos

Este repositório contém a implementação técnica do desafio proposto pela STGEN. O foco do desenvolvimento foi entregar uma arquitetura desacoplada e fácil de testar, simulando o fluxo real de uma operação de lanchonete.

---

## 🛠 Tech Stack

- **Runtime:** .NET 8 (LTS) / ASP.NET Core
- **Frontend:** Blazor WebAssembly (WASM)
- **Persistência:** EF Core (In-Memory Provider)
- **Quality Assurance:** xUnit
- **API Specs:** Swagger / OpenAPI

---

## 🏗 Arquitetura e Design Choices

Em vez de sobrecarregar o projeto com camadas desnecessárias, apliquei padrões que garantem manutenção e testabilidade:

- **Service Layer Pattern:** A lógica complexa de precificação e descontos foi isolada no `CalculadoraDescontoService`. Isso mantém os controllers magros e facilita a escrita de testes unitários.
- **Desacoplamento com DTOs:** Utilizei Data Transfer Objects para garantir que as entidades de banco de dados não sejam expostas diretamente. Isso protege o contrato da API e dá flexibilidade para evoluir o modelo interno.
- **Estratégia de Infra:** O uso do _In-Memory Database_ foi uma decisão deliberada para este desafio. O objetivo é permitir que o revisor execute o projeto imediatamente após o clone, sem dependências externas como Docker ou instâncias de SQL local.
- **Validações Semânticas:** O sistema valida regras de negócio (como a restrição de um item por categoria) e responde com status codes apropriados, seguindo as melhores práticas de APIs RESTful.

---

## 🍔 Regras de Negócio (Combos)

O motor de cálculo identifica automaticamente as seguintes combinações para aplicar descontos:

| Combo                             | Desconto |
| :-------------------------------- | :------- |
| Sanduíche + Batata + Refrigerante | **20%**  |
| Sanduíche + Refrigerante          | **15%**  |
| Sanduíche + Batata                | **10%**  |

---

## 🚀 Setup do Projeto

**Pré-requisito:** [.NET 11 SDK](https://dotnet.microsoft.com/download/dotnet/11.0) instalado.

### 1. Backend (API)

```bash
cd GoodHamburger.Api
dotnet run
```

_Swagger disponível em: `http://localhost:5000/swagger`_

### 2. Frontend (Blazor)

```bash
cd GoodHamburger.Web
dotnet run
```

_Interface disponível em: `http://localhost:5178`_

### 3. Testes Unitários

```bash
cd GoodHamburger.Tests
dotnet test
```

## 📚 Referências e Créditos

A estrutura base do frontend foi inspirada nos conceitos discutidos pelo professor Macoratti em sua [playlist sobre o BLAZOR](https://youtube.com/playlist?list=PL8Tes0ciwiaRKdVMSZvq-8LlXNyMKQUIC&si=_4nB4rEex7euA2Lo).
