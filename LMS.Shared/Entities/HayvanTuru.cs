using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("HayvanTuru")]
    public class HayvanTuru
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HayvanTuruID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string TurAdi { get; set; } = string.Empty; // Sığır, Koyun, Keçi, Manda
        
        [MaxLength(200)]
        public string? Aciklama { get; set; }

        public virtual ICollection<Irk> Irklar { get; set; } = new List<Irk>();
    }
}