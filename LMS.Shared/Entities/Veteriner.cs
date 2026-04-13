using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Veteriner")]
    public class Veteriner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VeterinerID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Ad { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Soyad { get; set; } = string.Empty;
        
        [MaxLength(15)]
        public string? Telefon { get; set; }
        
        [MaxLength(100)]
        public string? Uzmanlik { get; set; }
        
        [MaxLength(250)]
        public string? Adres { get; set; }

        public virtual ICollection<Muayene> Muayeneler { get; set; } = new List<Muayene>();
        public virtual ICollection<AsiKaydi> AsiKayitlari { get; set; } = new List<AsiKaydi>();
    }
}