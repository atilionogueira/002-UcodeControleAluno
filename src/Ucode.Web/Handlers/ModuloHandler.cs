using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class ModuloHandler(IHttpClientFactory httpClientFactory) : IModuloHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<PagedResponse<List<Modulo>?>> GetAllAsync(GetAllModuloRequest request)
             => await _client.GetFromJsonAsync<PagedResponse<List<Modulo>?>>("v1/modulos")
             ?? new PagedResponse<List<Modulo>?>(null, 400, "Não foi possível obter os modulos");
        
        
        public async Task<Response<Modulo?>> CreateAsync(CreateModuloRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/modulos", request);
            return await result.Content.ReadFromJsonAsync<Response<Modulo?>>()
                ?? new Response<Modulo?>(null, 400, "Falha ao criar o curso");
        }
        public async Task<Response<Modulo?>> UpdateAsync(UpdateModuloRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/modulos/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Modulo?>>()
                ?? new Response<Modulo?>(null, 400, "Falha ao atualizar o curso");
        }
        public async Task<Response<Modulo?>> DeleteAsync(DeleteModuloRequest request)
        {
            var result = await _client.DeleteAsync($"v1/modulos/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Modulo?>>()
                ?? new Response<Modulo?>(null, 400, "Falha ao excluir o curso");
        }     
                
        public async Task<Response<Modulo?>> GetByAsync(GetModuloByIdRequest request)
             => await _client.GetFromJsonAsync<Response<Modulo?>>($"v1/modulos/{request.Id}")
               ?? new Response<Modulo?>(null, 400, "Não foi possível obter o modulo");
    }
}
