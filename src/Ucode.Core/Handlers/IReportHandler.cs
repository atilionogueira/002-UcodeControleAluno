
using Ucode.Core.Models.Reports;
using Ucode.Core.Requests.Reports;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IReportHandler
    {
        Task<Response<List<ConcluidoAndAConcluir>?>> GetConcluidoAndAConcluirAsync(GetConcluidoAndAConcluirRequest request);
    }
}
