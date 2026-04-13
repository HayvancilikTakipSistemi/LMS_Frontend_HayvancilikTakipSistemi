namespace LMS.Shared.DTOs.Auth
{
    /// <summary>
    /// Response DTO for successful login/auth operations
    /// </summary>
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public int FarmerId { get; set; }
        public string Role { get; set; } = "Farmer";
    }
}
