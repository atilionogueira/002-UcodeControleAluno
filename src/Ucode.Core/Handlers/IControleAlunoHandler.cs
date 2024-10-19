using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IControleAlunoHandler
    {
        Task<Response<ControleAluno?>>CreateAsync(CreateControleAlunoRequest request);
        Task<Response<ControleAluno?>> UpdateAsync(UpdateControleAlunoRequest request);
        Task<Response<ControleAluno?>> DeleteAsync(DeleteControleAlunoRequest request);
        Task<Response<ControleAluno?>> GetByIdAsync(GetControleAlunoByIdRequest request);
        Task<Response<List<ControleAluno>?>> GetAllAsync(GetControleAlunoByPeriodRequest request);

    }
}
