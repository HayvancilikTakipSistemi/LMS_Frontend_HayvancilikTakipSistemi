using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("HayvanDurumu")]
    public class HayvanDurumu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HayvanDurumID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string DurumAdi { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? Aciklama { get; set; }

        public virtual ICollection<Hayvan> Hayvanlar { get; set; } = new List<Hayvan>();
    }
}