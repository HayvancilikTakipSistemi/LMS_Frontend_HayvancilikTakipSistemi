using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class FarmerDto
    {
        public int FarmerID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int AnimalCount { get; set; }

        public int BarnCount { get; set; }
    }
}
