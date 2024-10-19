
namespace Ucode.Core.Models
{
    public class Aluno
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public virtual ICollection<ControleAluno> ControleAlunos { get; set; } = new List<ControleAluno>();

    }
       
}
