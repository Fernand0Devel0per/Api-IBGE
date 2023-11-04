# 🚀 IBGE API

Bem-vindo à IBGE API, uma solução robusta para gerenciamento de endereços e usuários, construída com ASP.NET 7.0 utilizando a abordagem Minimal API. A API é projetada com foco em práticas recomendadas como Clean Code e Clean Architecture, além de aderir aos princípios do Domain-Driven Design (DDD). Ela emprega autenticação JWT e faz uso de caching com Redis para um desempenho otimizado.

# 🌟 Funcionalidades

- Autenticação JWT para segurança.
- CRUD completo para endereços e usuários.
- Filtragem de endereços por código IBGE, cidade ou estado.
- Cache de dados com Redis.
- Documentação completa e interativa com Swagger.

# 🚀 Configuração Inicial

### Para começar, siga as instruções abaixo:

1. Clone o repositório para sua máquina local.
2. Navegue até a pasta `Infra` e inicie os serviços do Docker com:
 
       docker-compose up -d 
   
    Execute os esquemas do banco de dados na pasta SqlServer para configurar o SQL Server.
    Configure a string de conexão do SQL Server no arquivo appsettings.json.
    Restaure os pacotes necessários com:

        dotnet restore 

Inicie a aplicação com:

      dotnet run 

A API estará disponível em http://localhost:5000.

# 📚 Endpoints

### A API fornece diversos endpoints para a gestão de endereços e usuários, bem como para a autenticação. Exemplos incluem:

    Autenticação:
        POST /api/authenticate: Autentica um usuário e retorna um token JWT.

    Endereços:
        GET /api/address/{ibgeCode}: Obtém um endereço pelo código do IBGE.
        POST /api/address: Cria um novo endereço.
        PUT /api/address: Atualiza um endereço existente.
        DELETE /api/address/{ibgeCode}: Deleta um endereço pelo código do IBGE.

    Usuários:
        POST /api/user: Cria um novo usuário.
        PUT /api/user: Atualiza um usuário existente.
        DELETE /api/user: Deleta um usuário.

A autenticação é necessária para acessar a maioria dos endpoints. As roles Admin e UserDefault são usadas para controle de acesso.

### Consulte a documentação do Swagger para obter uma lista completa e detalhada dos endpoints disponíveis.

# 🛠️ Tecnologias

    .NET 7
    Docker
    SQL Server
    Redis
    JWT Authentication
    Dapper
    Swagger

# 📖 Conclusão

Esta API representa uma abordagem moderna e eficiente para o gerenciamento de endereços e usuários, facilitando a integração com sistemas existentes e oferecendo uma plataforma escalável para crescer conforme suas necessidades. A documentação do Swagger oferece uma visão interativa para explorar todos os aspectos da API.

Para qualquer dúvida, sugestão ou contribuição, por favor, abra uma issue ou submeta um pull request. Sua colaboração é muito bem-vinda!
