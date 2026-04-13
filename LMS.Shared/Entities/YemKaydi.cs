using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("YemKaydi")]
    public class YemKaydi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YemKayitID { get; set; }
        
        public int HayvanID { get; set; }
        public int YemID { get; set; }
        
        [Column(TypeName = "decimal(6,2)")]
        [Required]
        public decimal MiktarKg { get; set; }
        
        [Required]
        public DateTime KayitTarihi { get; set; }

        public virtual Hayvan? Hayvan { get; set; }
        public virtual Yem? Yem { get; set; }
    }
}