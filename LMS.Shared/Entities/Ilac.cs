using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Ilac")]
    public class Ilac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IlacID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string IlacAdi { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string Birim { get; set; } = string.Empty; // ml, mg, tablet
        
        [MaxLength(200)]
        public string? Aciklama { get; set; }

        public virtual ICollection<MuayeneIlac> MuayeneIlaclar { get; set; } = new List<MuayeneIlac>();
    }
}