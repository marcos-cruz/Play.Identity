# Play.Identity

Play.Identity é um microserviço escrito em .net 8.0, que fornece suporte para registro, autenticação e autorização de usuários da aplicação Play.

## Ambiente de Desenvolvimento

| TOOL                                                      | DESCRIPTION                                     |
| :-------------------------------------------------------- | :---------------------------------------------- |
| [Ubuntu 24.04.3 LTS](https://ubuntu.com/download/desktop) | Sistema operacional desktop                     |
| [Visual Studio Code](https://aka.ms/vscode)               | IDE para desenvolvimento                        |
| [.Net 8.0 SDK](https://dotnet.microsoft.com/download)     | Kit para desenvolvimento do software            |
| [Docker](https://docs.docker.com/get-started/)            | Serviço provedor de container de infraestrutura |

[Back](#playidentity)


## Objetivos

### Módulo 1

* Entender o ASP.NET Core Identity (membership system)
* Integrar o ASP.NET Core Identity com MongoDB
* Habilitar o registro e login de usuário
* Adicionar REST API endpoints para usuários

## Bibliotecas

```powershell
dotnet tool install -g dotnet-aspnet-codegenerator --version 8.0.0

Skipping NuGet package signature verification.
You can invoke the tool using the following command: dotnet-aspnet-codegenerator
Tool 'dotnet-aspnet-codegenerator' (version '8.0.0') was successfully installed.
```

```powershell
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.0
```

```powershell
dotnet add package Microsoft.AspNetCore.Identity.UI --version 8.0.0
```

```powershell
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
```

```powershell
dotnet aspnet-codegenerator identity --files "Account.Register"
```