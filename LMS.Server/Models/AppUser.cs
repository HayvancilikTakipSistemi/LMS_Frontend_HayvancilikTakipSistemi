using Microsoft.AspNetCore.Identity;

namespace LMS.Server.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        
        public int? FarmerId { get; set; }
    }
}