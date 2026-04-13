using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("YemTuru")]
    public class YemTuru
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YemTuruID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string TurAdi { get; set; } = string.Empty; // Kuru Ot, Yoğun Yem, Silaj...
        
        [MaxLength(100)]
        public string? Aciklama { get; set; }

        public virtual ICollection<Yem> Yemler { get; set; } = new List<Yem>();
    }
}