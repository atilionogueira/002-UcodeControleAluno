using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class ControleAlunoHandler(IHttpClientFactory httpClientFactory) : IControleAlunoHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<PagedResponse<List<ControleAluno>?>> GetAllAsync(GetAllControleAlunoRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<ControleAluno>?>>("v1/controlealunos")
           ?? new PagedResponse<List<ControleAluno>?>(null, 400, "Não foi possível obter todas os Controle de Alunos");
        
        public async Task<PagedResponse<List<ControleAluno>?>> GetPeridAsync(GetControleAlunoByPeriodRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<ControleAluno>?>>("v1/controlealunos")
           ?? new PagedResponse<List<ControleAluno>?>(null, 400, "Não foi possível obter todas os Controle de Alunos");    
        

        public async Task<Response<ControleAluno?>> GetByIdAsync(GetControleAlunoByIdRequest request)
        => await _client.GetFromJsonAsync<Response<ControleAluno?>>($"v1/controlealunos/{request.Id}")
           ?? new Response<ControleAluno?> (null, 400, "Nao foi possível obter o Controle de Aluno"); 

        public async Task<Response<ControleAluno?>> CreateAsync(CreateControleAlunoRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/controlealunos", request);
            return await result.Content.ReadFromJsonAsync<Response<ControleAluno?>>()
                   ?? new Response<ControleAluno?> (null,400,"Falha ao criar o Controle de Aluno" );
        }            
               
        public async Task<Response<ControleAluno?>> UpdateAsync(UpdateControleAlunoRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/controlealunos/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<ControleAluno?>>()
                   ?? new Response<ControleAluno?> (null, 400, "Falha ao atualizar o Controle de Aluno");
        }

        public async Task<Response<ControleAluno?>> DeleteAsync(DeleteControleAlunoRequest request)
        {
            var result = await _client.DeleteAsync($"v1/controlealunos/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<ControleAluno?>>()
                   ?? new Response<ControleAluno?>(null, 400, "Falha ao excluir o Controle de Aluno");
        }     
    }
}
