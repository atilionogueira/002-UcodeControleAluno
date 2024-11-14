using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models.Reports;
using Ucode.Core.Requests.Reports;
using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<List<ConcluidoAndAConcluir>?>> GetConcluidoAndAConcluirAsync(GetConcluidoAndAConcluirRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<ConcluidoAndAConcluir>?>>($"v1/reports/expenses")
                ?? new Response<List<ConcluidoAndAConcluir>?>(null, 400, "Não foi possivel obter os dados");
        }
    }
}
