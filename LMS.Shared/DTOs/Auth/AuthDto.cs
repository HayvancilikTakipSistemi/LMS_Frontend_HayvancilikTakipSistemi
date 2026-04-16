using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs.Auth
{
    /// <summary>
    /// Combined DTO for both register and login operations
    /// </summary>
    public class AuthDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        // Register-only fields
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }
    }

    /// <summary>
    /// Legacy DTO for backwards compatibility (if needed)
    /// </summary>
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role selection is required.")]
        public int RoleId { get; set; } // 1: Farmer, 2: Veteriner, 3: Admin

        public string? Email { get; set; }
        public string? FullName { get; set; }
        public int? FarmerId { get; set; }
    }

    /// <summary>
    /// Legacy DTO for backwards compatibility (if needed)
    /// </summary>
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
