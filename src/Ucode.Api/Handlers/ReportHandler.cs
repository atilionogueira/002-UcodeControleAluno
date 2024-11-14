using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models.Reports;
using Ucode.Core.Requests.Reports;
using Ucode.Core.Responses;


namespace Ucode.Api.Handlers
{
    public class ReportHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Response<List<ConcluidoAndAConcluir>?>> GetConcluidoAndAConcluirAsync(GetConcluidoAndAConcluirRequest request)
        {
            try
            {
                var data = await context
                .ConcluidoAndAConcluirs
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Ano)
                .ThenBy(x => x.Mes)
                .ToListAsync();

                return new Response<List<ConcluidoAndAConcluir>?>(data);
            }
            catch
            {
                return new Response<List<ConcluidoAndAConcluir>?>(null, 500, "Não foi possível obter Status de Concluir e A Concluir");
            }
        }
    }
}
