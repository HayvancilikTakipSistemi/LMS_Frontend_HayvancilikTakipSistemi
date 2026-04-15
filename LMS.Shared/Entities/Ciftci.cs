using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Ciftci")]
    public class Ciftci
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CiftciID { get; set; }
        
        [MaxLength(11)]
        public string? TCKimlikNo { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Ad { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Soyad { get; set; } = string.Empty;
        
        [MaxLength(15)]
        public string? Telefon { get; set; }
        
        [MaxLength(250)]
        public string? Adres { get; set; }
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<Hayvan> Hayvanlar { get; set; } = new List<Hayvan>();
        public virtual ICollection<Ahir> Ahirlar { get; set; } = new List<Ahir>();
        public virtual ICollection<Satis> Satislar { get; set; } = new List<Satis>();
        public virtual ICollection<Personel> Personelleri { get; set; } = new List<Personel>();
    }
}
