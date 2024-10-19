using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IAlunoHandler
    {
        Task<Response<Aluno?>> CreateAsync(CreateAlunoRequest request);
        Task<Response<Aluno?>> UpdateAsync(UpdateAlunoRequest request);
        Task<Response<Aluno?>> DeleteAsync(DeleteAlunoRequest request);
        Task<Response<Aluno?>> GetByIdAsync(GetAlunoByIdRequest request);
        Task<PagedResponse<List<Aluno>>> GetAllAsync(GetAllAlunoRequest request);
    }

}
