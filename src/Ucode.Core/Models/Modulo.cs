namespace Ucode.Core.Models
{
    public class Modulo
    {
        public long Id { get; set; }
        public string SubTopico { get; set; } = string.Empty;       
        public string Secao { get; set; } = string.Empty;
        public long CursoId { get; set; }
        public Curso Curso { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;              
   

    }
}
