
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.ControleAluno
{
    public class UpdateControleAlunoRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Data inválida")]
        public DateTime DataInicio { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Data inválida")]
        public DateTime? DataFim { get; set; }

        [Required()]
        public string Resumo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Aluno inválido")]
        public long AlunoId { get; set; }

        [Required(ErrorMessage = "Curso inválido")]
        public long CursoId { get; set; }

        [Required(ErrorMessage = "Modulo inválido")]
        public long ModuloId { get; set; }
    }
}
