using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("SutKaydi")]
    public class SutKaydi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SutKayitID { get; set; }
        
        public int HayvanID { get; set; }
        
        [Required]
        public DateTime KayitTarihi { get; set; }
        
        [Column(TypeName = "decimal(6,2)")]
        [Required]
        public decimal MiktarLitre { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal? BirimFiyat { get; set; }
        
        [MaxLength(200)]
        public string? Notlar { get; set; }

        public virtual Hayvan? Hayvan { get; set; }
    }
}