using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class DrugDto
    {
        public int DrugID { get; set; }

        [Required]
        [MaxLength(100)]
        public string DrugName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
