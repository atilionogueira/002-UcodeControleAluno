
using Ucode.Core.Requests.Account;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IAccountHandler 
    {
        Task<Response<string>> LoginAsync(LoginRequest request);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
    }
}
