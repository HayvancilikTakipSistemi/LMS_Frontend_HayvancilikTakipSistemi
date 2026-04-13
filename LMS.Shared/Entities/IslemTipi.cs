using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("IslemTipi")]
    public class IslemTipi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IslemTipiID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string TipAdi { get; set; } = string.Empty; // Tahsilat, Ödeme, Avans
        
        [MaxLength(100)]
        public string? Aciklama { get; set; }

        public virtual ICollection<ParaIslem> ParaIslemler { get; set; } = new List<ParaIslem>();
    }
}