# SkyHorizon: Sistema de Gestão de Agência de Viagens Aéreas

## Visão Geral do Projeto

O **SkyHorizon** é um sistema de gestão de agência de viagens aéreas desenvolvido em **ASP.NET MVC** com **Entity Framework Core**. O projeto foi concebido para modernizar a gestão de voos, passageiros e reservas, implementando um robusto sistema de controlo de acessos baseado em papéis (Role-Based Access Control - RBAC).

Este sistema cumpre integralmente os requisitos do **Projeto Módulo 5** (Agência de Viagens Aéreas), oferecendo funcionalidades distintas para Administradores e Passageiros.

## Funcionalidades Principais

O sistema é dividido em duas áreas principais, com base no papel do utilizador:

### 1. Área do Administrador (Role: `Admin`)

Acesso total ao painel de gestão para operações de CRUD (Create, Read, Update, Delete):

| Funcionalidade | Descrição |
| --- | --- |
| **Gestão de Voos/Destinos** | CRUD completo para adicionar, editar e remover destinos de viagem, incluindo detalhes como datas, horários e preços. |
| **Gestão de Reservas** | Visualização e gestão de todas as reservas efetuadas no sistema, com opção de cancelamento. |
| **Gestão de Clientes** | Visualização e manutenção da lista de utilizadores registados (Passageiros). |
| **Dashboard** | Visão geral e estatísticas sobre o número total de destinos, reservas e clientes. |

### 2. Área do Passageiro (Role: `User`)

Acesso limitado a funcionalidades de consulta e reserva:

| Funcionalidade | Descrição |
| --- | --- |
| **Registo e Login** | Sistema de autenticação seguro, com atribuição automática do papel `User` no registo. |
| **Catálogo de Voos** | Visualização de todos os destinos disponíveis para reserva. |
| **Detalhe do Destino** | Consulta de informações detalhadas sobre um voo específico. |
| **Reservas** | Funcionalidade para efetuar novas reservas. |
| **Histórico de Reservas** | Visualização e gestão das reservas pessoais, com opção de cancelamento. |

## 🛠️ Tecnologias Utilizadas

- **Backend:** ASP.NET Core MVC (C#)

- **Base de Dados:** MySQL (configurado via Entity Framework Core com o provider Pomelo)

- **ORM:** Entity Framework Core

- **Autenticação:** Autenticação baseada em Cookies e Claims para RBAC.

## 🚀 Como Executar o Projeto

### Pré-requisitos

Para executar este projeto, necessita de ter instalado:

1. **.NET SDK** (versão 6.0 ou superior)

1. **MySQL Server** (ou um servidor compatível)

1. **Visual Studio** ou **Visual Studio Code** com as extensões C# e ASP.NET.

### Passos de Configuração

1. **Clonar o Repositório:**

   ```bash
   git clone <URL_DO_REPOSITORIO>
   cd 2223262_ProjetoFinalASPMVC_SkyHorizon_v2
   ```

1. **Configurar a Base de Dados:**
  - Abra o ficheiro `appsettings.json` (ou similar) e configure a *connection string* para o seu servidor MySQL.
    - O projeto utiliza o Entity Framework Core para a gestão da base de dados. Execute as *migrations* para criar o esquema da base de dados:
    
       ```bash
       dotnet ef database update
       ```
    
       *(Nota: Se as migrations não estiverem incluídas, o esquema será criado na primeira execução, dependendo da configuração do **`DbContext`**.)*

1. **Executar a Aplicação:**

   ```bash
   dotnet run --project 2223262_ProjetoFinalASPMVC_SkyHorizon/2223262_ProjetoFinalASPMVC_SkyHorizon.csproj
   ```

1. **Aceder à Aplicação:**A aplicação estará disponível em `https://localhost:<PORTA_DO_PROJETO>`.

### Credenciais de Teste (Exemplo )

Para testar as funcionalidades de Administrador, poderá ser necessário criar um utilizador com a `Role` definida como `Admin` diretamente na base de dados, ou verificar se existe um *seed* de dados inicial.

## Estrutura do Projeto

```
.
├── 2223262_ProjetoFinalASPMVC_SkyHorizon/
│   ├── Controllers/
│   │   ├── AdminController.cs  # Lógica de gestão (CRUD Admin)
│   │   └── HomeController.cs   # Lógica de utilizador (Catálogo, Reservas, Auth)
│   ├── Models/
│   │   ├── User.cs             # Modelo de Utilizador (com Role)
│   │   ├── Destination.cs      # Modelo de Voo/Destino
│   │   ├── Booking.cs          # Modelo de Reserva
│   │   └── SkyHorizonContext.cs# Contexto da Base de Dados
│   ├── Views/
│   │   ├── Admin/
│   │   └── Home/
│   ├── wwwroot/                # Ficheiros estáticos (CSS, JS, Imagens)
│   └── Program.cs              # Configuração da aplicação
├── 2223262_ProjetoFinalASPMVC_SkyHorizon_v2.sln
└── README.md
```

---

*Desenvolvido em conformidade com os requisitos do Módulo 5.*

2223262 - Victor Martins
