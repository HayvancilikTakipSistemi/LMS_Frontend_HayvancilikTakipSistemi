using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Muayene")]
    public class Muayene
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MuayeneID { get; set; }
        
        public int HayvanID { get; set; }
        public int VeterinerID { get; set; }
        
        [Required]
        public DateTime MuayeneTarihi { get; set; }
        
        [MaxLength(300)]
        public string? Teshis { get; set; }
        
        [MaxLength(300)]
        public string? Tedavi { get; set; }
        
        [MaxLength(500)]
        public string? Notlar { get; set; }

        public virtual Hayvan? Hayvan { get; set; }
        public virtual Veteriner? Veteriner { get; set; }
        public virtual ICollection<MuayeneIlac> MuayeneIlaclar { get; set; } = new List<MuayeneIlac>();
    }
}