using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("SatisDetay")]
    public class SatisDetay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SatisDetayID { get; set; }
        
        public int SatisID { get; set; }
        public int UrunTuruID { get; set; }
        public int? HayvanID { get; set; } // Canlı hayvan satışında dolu
        
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal Miktar { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal BirimFiyat { get; set; }
        
        [Column(TypeName = "decimal(21,2)")] // Miktar * BirimFiyat
        public decimal Tutar { get; set; } // Hesaplanmış alan

        public virtual Satis? Satis { get; set; }
        public virtual UrunTuru? UrunTuru { get; set; }
        public virtual Hayvan? Hayvan { get; set; }
    }
}