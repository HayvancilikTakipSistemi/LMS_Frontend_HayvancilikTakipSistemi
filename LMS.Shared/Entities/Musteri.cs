using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Musteri")]
    public class Musteri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MusteriID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AdSoyad { get; set; } = string.Empty;
        
        [MaxLength(15)]
        public string? Telefon { get; set; }
        
        [MaxLength(250)]
        public string? Adres { get; set; }
        
        [MaxLength(50)]
        public string? MusteriTipi { get; set; } // Bireysel, Kurumsal
        
        [MaxLength(300)]
        public string? Notlar { get; set; }

        public virtual ICollection<Satis> Satislar { get; set; } = new List<Satis>();
        public virtual ICollection<ParaIslem> ParaIslemler { get; set; } = new List<ParaIslem>();
    }
}