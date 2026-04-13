using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Shared.Entities
{
    [Table("Personel")]
    public class Personel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonelID { get; set; }
        
        public int CiftciID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AdSoyad { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? Gorev { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Maas { get; set; }
        
        public DateTime? IseBaslama { get; set; }
        
        [MaxLength(15)]
        public string? Telefon { get; set; }

        public virtual Ciftci? Ciftci { get; set; }
    }
}