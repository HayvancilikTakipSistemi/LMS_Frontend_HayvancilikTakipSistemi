using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("UrunTuru")]
    public class UrunTuru
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UrunTuruID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string TurAdi { get; set; } = string.Empty; // Süt, Et, Yün, Deri, Canlı Hayvan
        
        [Required]
        [MaxLength(20)]
        public string Birim { get; set; } = string.Empty; // Litre, Kg, Adet

        public virtual ICollection<SatisDetay> SatisDetaylari { get; set; } = new List<SatisDetay>();
    }
}