using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("FeedRecords")]
    public class FeedRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedRecordID { get; set; }
        
        public int AnimalID { get; set; }
        public int FeedID { get; set; }
        
        [Column(TypeName = "decimal(6,2)")]
        [Required]
        public decimal QuantityKg { get; set; }
        
        [Required]
        public DateTime RecordDate { get; set; }

        [ForeignKey("AnimalID")]
        public virtual Animal? Animal { get; set; }
        
        [ForeignKey("FeedID")]
        public virtual Feed? Feed { get; set; }
    }
}
