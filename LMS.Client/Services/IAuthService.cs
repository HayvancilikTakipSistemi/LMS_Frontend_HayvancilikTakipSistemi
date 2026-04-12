using LMS.Shared.DTOs.Auth;
using System.Threading.Tasks;

namespace LMS.Client.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task LogoutAsync();
    }
}
