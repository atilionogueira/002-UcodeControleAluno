
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Modulo
{
    public class UpdateModuloRequest : Request
    {
        public long Id { get; set; } 

        [Required(ErrorMessage = "Título inválido")]
        public string SubTopico { get; set; } = string.Empty;       

        [Required]
        public string Secao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Curso Inválido")]
        public long CursoId { get; set; }
    }
}
