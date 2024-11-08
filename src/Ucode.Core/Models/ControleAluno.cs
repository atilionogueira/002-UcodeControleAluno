
using Ucode.Core.Enums;

namespace Ucode.Core.Models
{
    public class ControleAluno
    {
        public long Id { get; set; }       
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
        public string Resumo { get; set; } = string.Empty;

        public EStatus Status { get; set; } = EStatus.AConcluir;


        public long CursoId { get; set; }
        public Curso Curso { get; set; } = null!;
        public long ModuloId { get; set; }
        public Modulo Modulo { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;      

    }
}
