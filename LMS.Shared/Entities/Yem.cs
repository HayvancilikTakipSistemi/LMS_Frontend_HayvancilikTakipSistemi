using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Yem")]
    public class Yem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YemID { get; set; }
        
        public int YemTuruID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string YemAdi { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal StokMiktar { get; set; } = 0;
        
        [Required]
        [MaxLength(20)]
        public string Birim { get; set; } = string.Empty; // Kg, Ton

        public virtual YemTuru? YemTuru { get; set; }
        public virtual ICollection<YemKaydi> YemKayitlari { get; set; } = new List<YemKaydi>();
    }
}