
namespace Ucode.Core.Models.Account
{
    public class User  // pegar informação de v1/identity/manage/info
    {
        public string Email { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; }
        public Dictionary<string, string> Claims { get; set; } = []; // Pega Tipo e Valor
    }
}
