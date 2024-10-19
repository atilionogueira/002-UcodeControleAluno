using System.Collections.Generic;
using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class AlunoHandler(IHttpClientFactory httpClientFactory) : IAlunoHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<PagedResponse<List<Aluno>>> GetAllAsync(GetAllAlunoRequest request)
           => await _cliente.GetFromJsonAsync<PagedResponse<List<Aluno>>>("v1/alunos")
           ?? new PagedResponse<List<Aluno>>(null, 400, "Não foi possível obter os alunos");


        public async Task<Response<Aluno?>> GetByIdAsync(GetAlunoByIdRequest request)
            => await _cliente.GetFromJsonAsync<Response<Aluno?>>($"v1/alunos/{request.Id}")
            ?? new Response<Aluno?>(null, 404, "Não foi possível obter o aluno");      

        private readonly HttpClient _cliente = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Aluno?>> CreateAsync(CreateAlunoRequest request)
        {
            var result = await _cliente.PostAsJsonAsync("v1/alunos", request);
            return await result.Content.ReadFromJsonAsync<Response<Aluno?>>()
                ?? new Response<Aluno?>(null, 400, "Falha ao criar o aluno");
        }

        public async Task<Response<Aluno?>> UpdateAsync(UpdateAlunoRequest request)
        {
            var result = await _cliente.PutAsJsonAsync($"v1/alunos/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Aluno?>>()
                ?? new Response<Aluno?>(null, 400, "Falha ao atualizar o aluno");
        }
        public async Task<Response<Aluno?>> DeleteAsync(DeleteAlunoRequest request)
        {
            var result = await _cliente.DeleteAsync($"v1/alunos/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Aluno?>>()
                ?? new Response<Aluno?>(null, 400, "Falha ao atualizar o aluno");
        }

       

       
    }
}
