using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface ICursoHandler
    {
        Task<Response<Curso?>> CreateAsync(CreateCursoRequest request);
        Task<Response<Curso?>> UpdateAsync(UpdateCursoRequest request);
        Task<Response<Curso?>> DeleteAsync(DeleteCursoRequest request);
        Task<Response<Curso?>> GetByIdAsync(GetCursoByIdRequest request);
        Task<PagedResponse<List<Curso>>> GetAllAsync(GetAllCursoRequest request);       
    }
}
