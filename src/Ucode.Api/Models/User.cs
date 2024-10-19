using Microsoft.AspNetCore.Identity;

namespace Ucode.Api.Models
{
    public class User : IdentityUser<long>
    {
        // Pode colocar qq atribudo do Usuário
        public List<IdentityRole<long>>? Roles { get; set; }
    }
}
