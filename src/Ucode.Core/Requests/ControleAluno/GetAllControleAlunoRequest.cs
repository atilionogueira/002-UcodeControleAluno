namespace Ucode.Core.Requests.ControleAluno
{
    public class GetAllControleAlunoRequest : PagedRequest
    {
        public long CursoId { get; set; }
        public long ModuloId { get; set; }
    }
}
