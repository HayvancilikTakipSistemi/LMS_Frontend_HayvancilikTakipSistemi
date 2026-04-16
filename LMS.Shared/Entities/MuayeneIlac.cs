using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("MuayeneIlac")]
    public class MuayeneIlac
    {
        public int MuayeneID { get; set; }
        public int IlacID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Doz { get; set; } = string.Empty; // "5 ml", "2 tablet"
        
        public DateTime? SureBitis { get; set; } // İlaç bitiş tarihi

        public virtual Muayene? Muayene { get; set; }
        public virtual Ilac? Ilac { get; set; }
    }
}