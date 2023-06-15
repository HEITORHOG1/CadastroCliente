# Sistema de Registro de Usuários - CadastroCliente API

O sistema de registro de usuários, também conhecido como CadastroCliente API, é uma API RESTful que fornece serviços para gerenciar o cadastro de clientes.

## Características

- Obter todos os usuários.
- Obter detalhes de um único usuário.
- Criar um novo usuário.
- Atualizar detalhes de um usuário existente.
- Deletar um usuário existente.
- Pesquisar usuários.
- Autenticação de usuários com JWT (Json Web Token).
- Registro de logs de eventos e erros utilizando o Microsoft ILogger.
- Integração com Azure Application Insights para análise de logs e telemetria.

## Tecnologias

- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MySQL](https://www.mysql.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)
- [Swashbuckle (Swagger)](https://swagger.io/tools/swagger-ui/)
- [Microsoft ILogger](https://docs.microsoft.com/en-us/dotnet/core/extensions/logging)
- [Azure Application Insights](https://azure.microsoft.com/services/application-insights/)

## Arquitetura e Estrutura de Projetos

O projeto segue uma arquitetura baseada no padrão de design Clean Architecture, o qual visa a separação de responsabilidades, desacoplamento e a facilitação de testes automatizados. O projeto está estruturado em camadas, conforme segue:

- **CadastroCliente.Api**: Camada de apresentação da API, responsável pelo tratamento de requisições HTTP e respostas.
- **CadastroCliente.Services**: Camada de serviços, contendo a lógica de negócio principal.
- **CadastroCliente.Model**: Camada que contém as classes de domínio do projeto.
- **CadastroCliente.Data**: Camada de acesso a dados, responsável pela persistência e recuperação de dados.

## Documentação da API

A documentação da API está disponível através do Swagger UI, que pode ser acessado ao executar o projeto localmente e navegando para o endpoint `/swagger`.

## Como Usar

Certifique-se de ter a versão apropriada do .NET Core SDK instalada em seu sistema. Além disso, você precisará de um servidor MySQL em execução e uma chave de instrumentação do Azure Application Insights.

1. Clone este repositório para sua máquina local.
2. Navegue até o diretório do projeto via terminal.
3. Execute `dotnet restore` para restaurar os pacotes necessários.
4. Configure a string de conexão para o seu servidor MySQL no arquivo `appsettings.json`.
5. Adicione a chave de instrumentação do Azure Application Insights ao arquivo `appsettings.json`.
6. Execute `dotnet run` para iniciar a aplicação.

A aplicação estará disponível no endereço `http://localhost:5000` (ou `(https://localhost:7069/)` para HTTPS).

## Contribuição

Para contribuir com o projeto, faça um fork do repositório, faça suas alterações e envie um pull request. As alterações serão revisadas e incorporadas conforme apropriado.

## Licença

Este projeto está sob a licença XYZ. Veja o arquivo `LICENSE` para mais detalhes.
