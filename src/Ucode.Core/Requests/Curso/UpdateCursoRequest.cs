using System.ComponentModel.DataAnnotations;
using Ucode.Core.Enums;

namespace Ucode.Core.Requests.Curso
{
    public class UpdateCursoRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ("Curso Inválido"))]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = ("Categoria inválida"))]
        public ECursoCategoria Categoria { get; set; }

        [Required]
        public string? Resumo { get; set; } 
    }
}
