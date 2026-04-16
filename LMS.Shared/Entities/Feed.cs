using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Feeds")]
    public class Feed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedID { get; set; }
        
        public int FeedTypeID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FeedName { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal StockQuantity { get; set; } = 0;
        
        [Required]
        [MaxLength(20)]
        public string Unit { get; set; } = string.Empty; // Kg, Ton

        [ForeignKey("FeedTypeID")]
        public virtual FeedType? FeedType { get; set; }
        
        public virtual ICollection<FeedRecord> FeedRecords { get; set; } = new List<FeedRecord>();
    }
}
