using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class VeterinarianDto
    {
        public int VeterinarianID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Specialization { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
