using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Satis")]
    public class Satis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SatisID { get; set; }
        
        public int MusteriID { get; set; }
        public int CiftciID { get; set; }
        
        [Required]
        public DateTime SatisTarihi { get; set; }
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal? ToplamTutar { get; set; }
        
        [MaxLength(300)]
        public string? Notlar { get; set; }

        public virtual Musteri? Musteri { get; set; }
        public virtual Ciftci? Ciftci { get; set; }
        public virtual ICollection<SatisDetay> SatisDetaylari { get; set; } = new List<SatisDetay>();
        public virtual ICollection<ParaIslem> ParaIslemler { get; set; } = new List<ParaIslem>();
    }
}