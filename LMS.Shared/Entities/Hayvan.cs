using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Hayvan")]
    public class Hayvan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HayvanID { get; set; }
        
        public int CiftciID { get; set; }
        public int? AhirID { get; set; }
        public int? IrkID { get; set; }
        public int HayvanDurumID { get; set; } = 1;
        
        [Required]
        [MaxLength(20)]
        public string KupeNo { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(1)]
        public string Cinsiyet { get; set; } = string.Empty; // 'E' or 'D'
        
        public DateTime? DogumTarihi { get; set; }
        public int? AnneID { get; set; }

        // Navigation Properties
        public virtual Ciftci? Ciftci { get; set; }
        public virtual Ahir? Ahir { get; set; }
        public virtual Irk? Irk { get; set; }
        public virtual HayvanDurumu? HayvanDurumu { get; set; }
        public virtual Hayvan? Anne { get; set; }
        
        public virtual ICollection<Hayvan> Yavrular { get; set; } = new List<Hayvan>();
        public virtual ICollection<Muayene> Muayeneler { get; set; } = new List<Muayene>();
        public virtual ICollection<AsiKaydi> AsiKayitlari { get; set; } = new List<AsiKaydi>();
        public virtual ICollection<SutKaydi> SutKayitlari { get; set; } = new List<SutKaydi>();
        public virtual ICollection<YemKaydi> YemKayitlari { get; set; } = new List<YemKaydi>();
        public virtual ICollection<SatisDetay> SatisDetaylari { get; set; } = new List<SatisDetay>();
    }
}
