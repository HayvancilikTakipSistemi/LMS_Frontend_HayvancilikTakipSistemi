using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class FeedDto
    {
        public int FeedID { get; set; }
        
        public int FeedTypeID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FeedName { get; set; } = string.Empty;
        
        [Required]
        public decimal StockQuantity { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Unit { get; set; } = string.Empty;
    }
}
