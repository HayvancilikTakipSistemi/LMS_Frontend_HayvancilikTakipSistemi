using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class FeedTypeDto
    {
        public int FeedTypeID { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
