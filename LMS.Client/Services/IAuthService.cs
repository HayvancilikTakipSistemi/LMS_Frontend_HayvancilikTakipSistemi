namespace LMS.Client.Services
{
    /// <summary>
    /// Authentication service interface for Blazor client
    /// </summary>
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string firstName, string lastName, string email, string password, string phoneNumber = "", string address = "");
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<string?> GetTokenAsync();
        Task<bool> IsAuthenticatedAsync();
        Task RefreshTokenAsync();
    }
}
