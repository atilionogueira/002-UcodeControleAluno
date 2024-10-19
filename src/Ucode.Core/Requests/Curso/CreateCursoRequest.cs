using System.ComponentModel.DataAnnotations;
using Ucode.Core.Enums;

namespace Ucode.Core.Requests.Curso

{
    public class CreateCursoRequest : Request
    {
        [Required(ErrorMessage = ("Curso Inválido"))]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = ("Categoria inválida"))]
        public ECursoCategoria Categoria { get; set; } = ECursoCategoria.Nenhuma;

        [Required]
        public string Resumo { get; set; } = string.Empty;
    }
}
