
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Alunos
{
    public class CreateAlunoRequest : Request
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O contato é obrigatório.")]
        [Phone(ErrorMessage = "O formato do contato é inválido.")]
        public string Contato { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "O Instagram deve ter no máximo 50 caracteres.")]
        public string Instagram { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(50, ErrorMessage = "A cidade deve ter no máximo 50 caracteres.")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2, ErrorMessage = "O estado deve ter 2 caracteres.")]
        public string Estado { get; set; } = string.Empty;
    }
}
