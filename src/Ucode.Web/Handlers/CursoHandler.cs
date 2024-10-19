using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class CursoHandler(IHttpClientFactory httpClientFactory) : ICursoHandler    
    {
    
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<PagedResponse<List<Curso>>> GetAllAsync(GetAllCursoRequest request)
             => await _client.GetFromJsonAsync<PagedResponse<List<Curso>>>("v1/cursos")
             ?? new PagedResponse<List<Curso>>(null, 400, "Não foi possível obter os cursos");

        public async Task<Response<Curso?>> GetByIdAsync(GetCursoByIdRequest request)
            => await _client.GetFromJsonAsync<Response<Curso?>>($"v1/cursos/{request.Id}")
               ?? new Response<Curso?>(null, 400, "Não foi possível obter o curso");
        
        public async Task<Response<Curso?>> CreateAsync(CreateCursoRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/cursos",request);
            return await result.Content.ReadFromJsonAsync<Response<Curso?>>()
                ?? new Response<Curso?>(null, 400, "Falha ao criar o curso");    
        }

        public async Task<Response<Curso?>> UpdateAsync(UpdateCursoRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/cursos/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Curso?>>()
                ?? new Response<Curso?>(null, 400, "Falha ao atualizar o curso");
        }

        public async Task<Response<Curso?>> DeleteAsync(DeleteCursoRequest request)
        {       
            var result = await _client.DeleteAsync($"v1/cursos/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Curso?>>()
                   ?? new Response<Curso?>(null, 400, "Falha ao excluir a categoria");
        }



    }
}
