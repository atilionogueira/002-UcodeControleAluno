
# UcodeControleAluno API

**Descrição**  
A API de Controle de Aluno foi desenvolvida em **ASP.NET Core 8** e integra-se a um sistema de gerenciamento acadêmico com funcionalidades de CRUD para cursos, módulos e alunos. Ela permite o acompanhamento de status de tarefas e a visualização de progresso por meio de gráficos, usando **MudBlazor** para o frontend. Este projeto segue uma arquitetura em três camadas, com um backend (`ucode.api`), um core (`ucode.core`) e um frontend em Blazor (`ucode.web`).

## Arquitetura

- **Backend** (`ucode.api`): Responsável pelos endpoints e lógica de negócios da API.
- **Core** (`ucode.core`): Contém as entidades e regras de negócio.
- **Frontend** (`ucode.web`): Interface do usuário desenvolvida com Blazor e MudBlazor.

## Estrutura de Pastas

- `/ucode.api/endpoint`: Configuração dos endpoints e rotas da API.
- `/ucode.api/handlers`: Implementação dos manipuladores das operações de CRUD.
- `/ucode.api/common/api`: Utilitários comuns para a API.
- `/ucode.api/data`: Acesso a dados e operações no banco de dados.

## Pré-requisitos

- **.NET 8 SDK**
- **Blazor 8.0.5 **
- **MudBlazor 6.19.1**
- **SQL Server** ou outro banco de dados relacional compatível
- **Visual Studio 2022** ou **Visual Studio Code**

## Configuração do Ambiente

1. Clone o repositório do GitHub:
   ```bash
   git clone https://github.com/atilionogueira/UcodeControleAluno.git
   ```
2. Configure as variáveis de ambiente para conexão com o banco de dados em `appsettings.json` no projeto `ucode.api`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=ControleAlunoDB;User Id=sa;Password=your_password;"
   }
   ```
3. Execute as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update --project ucode.api
   ```
4. Inicie a API:
   ```bash
   dotnet run --project ucode.api
   ```

## Autenticação e Autorização

A API utiliza autenticação baseada em tokens JWT para proteger os endpoints. Cada solicitação precisa incluir o cabeçalho `Authorization` com o token JWT obtido na autenticação.

Exemplo de cabeçalho:
```plaintext
Authorization: Bearer {seu_token_jwt}
```

## Endpoints Principais

### Autenticação

- **POST /api/auth/login**  
  Autentica o usuário e retorna um token JWT.

### Controle de Cursos

- **GET /api/v1/cursos**  
  Retorna uma lista de todos os cursos.
  
- **POST /api/v1/cursos**  
  Cria um novo curso.
  
- **GET /api/v1/cursos/{id}**  
  Retorna as informações detalhadas de um curso.
  
- **PUT /api/v1/cursos/{id}**  
  Atualiza um curso existente.
  
- **DELETE /api/v1/cursos/{id}**  
  Exclui um curso.

### Controle de Módulos

- **GET /api/v1/modulos**  
  Lista todos os módulos.

- **POST /api/v1/modulos**  
  Adiciona um novo módulo.

### Controle de Alunos

- **GET /api/v1/alunos**  
  Retorna uma lista de alunos.

- **POST /api/v1/alunos**  
  Adiciona um novo aluno.

### Controle de Tarefas

- **GET /api/v1/tarefas/status**  
  Retorna o status das tarefas de cada aluno com base no enum `EStatus`.

## Exemplos de Requisição

### Exemplo de Criação de Curso

```json
POST /api/v1/cursos
Content-Type: application/json
Authorization: Bearer {seu_token_jwt}

{
  "nome": "Curso de ASP.NET",
  "descricao": "Curso avançado de desenvolvimento com .NET",
  "dataInicio": "2024-01-01",
  "dataFim": "2024-06-30",
  "status": "A Concluir"
}
```

### Exemplo de Atualização de Status da Tarefa

```json
PUT /api/v1/tarefas/{id}
Content-Type: application/json
Authorization: Bearer {seu_token_jwt}

{
  "status": "Concluido"
}
```

## Contribuição

Contribuições são bem-vindas! Para contribuir:

1. Crie um fork do repositório.
2. Crie uma nova branch (`git checkout -b minha-feature`).
3. Faça suas modificações e commit (`git commit -am 'Minha nova feature'`).
4. Envie para a branch (`git push origin minha-feature`).
5. Crie um Pull Request.
