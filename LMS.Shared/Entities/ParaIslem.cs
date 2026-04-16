using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("ParaIslem")]
    public class ParaIslem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IslemID { get; set; }
        
        public int MusteriID { get; set; }
        public int? SatisID { get; set; }
        public int IslemTipiID { get; set; }
        
        [Column(TypeName = "decimal(12,2)")]
        [Required]
        public decimal Tutar { get; set; }
        
        [Required]
        public DateTime IslemTarihi { get; set; }
        
        [MaxLength(300)]
        public string? Aciklama { get; set; }

        public virtual Musteri? Musteri { get; set; }
        public virtual Satis? Satis { get; set; }
        public virtual IslemTipi? IslemTipi { get; set; }
    }
}