using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public class FeedRecordDto
    {
        public int FeedRecordID { get; set; }
        
        public int AnimalID { get; set; }
        public int FeedID { get; set; }
        
        [Required]
        public decimal QuantityKg { get; set; }
        
        [Required]
        public DateTime RecordDate { get; set; }
    }
}
